using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using FinanceApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/accounts
        // GET: api/accounts/?pageIndex=0&pageSize=10
        // GET: api/accounts/?pageIndex=0&pageSize=10&sortColumn=name&sortOrder=asc
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAccounts(
            int pageIndex = 0,
            int pageSize = 10,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/accounts/dto
        [HttpGet]
        [Route("dto")]
        public async Task<IEnumerable<AccountDto>> GetAccountDtos()
        {
            var dtos = new List<AccountDto>();
            var accounts = await _context.Accounts.ToListAsync();
            var bills = await _context.Bills.ToListAsync();
            var payDeductionDict = CalculationsService.GetPaycheckContributionsDict(accounts, bills);

            foreach (var account in accounts)
            {
                var dto = new AccountDto();
                dto.Account = account;
                dto.Bills = await bills.Where(b => b.AccountId == account.Id).ToListAsync();
                dto.BillSum = dto.Bills.Sum(b => b.AmountDue);

                payDeductionDict.TryGetValue(account.Name, out var payDeduction);

                dto.PayDeduction = payDeduction;
                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(long id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
                return NotFound();

            return account;
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount([FromBody] Account account)
        {
            try
            {
                //account.UserId = _userId;
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
