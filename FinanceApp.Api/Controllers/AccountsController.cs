using System;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccounts()
        {
            try
            {
               return Ok(await _accountRepository.GetAllAccountsAsync());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
