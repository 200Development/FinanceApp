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
        public async Task<DTO> GetAccountDto()
        {
            var dto = new DTO();
            var accountDtos = new List<AccountDto>();
            try
            {
                var accounts = await _context.Accounts.ToListAsync();
                var bills = await _context.Bills.ToListAsync();
                var payDeductionDict = CalculationsService.GetPayDeductionDict(accounts, bills);
                var requiredSavingsDict = CalculationsService.GetRequiredSavingsDict(payDeductionDict, bills);
                var sumOfAccountBalances = 0.0m;
                var totalSurplus = 0.0m;

                foreach (var account in accounts)
                {
                    var newDto = new AccountDto();
                    newDto.Account = account;
                    newDto.Bills = await bills.Where(b => b.AccountId == account.Id).ToListAsync();
                    newDto.BillSum = newDto.Bills.Sum(b => b.AmountDue);

                    payDeductionDict.TryGetValue(account.Id, out var payDeduction);

                    newDto.PayDeduction = payDeduction;
                    newDto.PaycheckPercentage = CalculationsService.GetAccountPaycheckPercentage(payDeductionDict, payDeduction);
                    newDto.ExpensesBeforeNextPaycheck = 63.43m;

                    requiredSavingsDict.TryGetValue(account.Id, out var accountRequiredSavings);
                    
                    newDto.RequiredSavings = accountRequiredSavings;

                    var accountSurplus = account.Balance - accountRequiredSavings;
                    newDto.BalanceSurplus = accountSurplus;

                    totalSurplus += accountSurplus;
                    sumOfAccountBalances += account.Balance;


                    accountDtos.Add(newDto);
                }

                dto.AccountDtos = accountDtos;
                dto.SumOfAccountBalances = accounts.Sum(a => a.Balance);
                dto.TotalSurplus = totalSurplus;


                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
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
