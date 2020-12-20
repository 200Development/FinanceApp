using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Api.Entities;
using FinanceApp.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountRepository _accountRepository;



        public AccountsController(ApplicationDbContext context)
        {
            _accountRepository = new AccountRepository(context);
        }


        // GET: api/Accounts
        // GET: api/Accounts/?pageIndex=0&pageSize=10
        // GET: api/Accounts/?pageIndex=0&pageSize=10&sortColumn=name&sortOrder=asc
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAccounts(
            int pageIndex = 0,
            int pageSize = 10,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await _accountRepository.GetAllAccountsAsync();

            
        }

        // POST: api/Accounts
        [HttpPost]
        public ActionResult CreateAccount(Account account)
        {
            try
            {
                _accountRepository.CreateAccount(account);

                return Ok("New Account Added");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
