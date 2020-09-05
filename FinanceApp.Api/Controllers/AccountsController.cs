using FinanceApp.Data;
using FinanceApp.Data.Repositories;
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
        //[HttpGet]
        //public async Task<ActionResult<ApiResult<Account>>> GetAccounts(
        //    int pageIndex = 0,
        //    int pageSize = 10,
        //    string sortColumn = null,
        //    string sortOrder = null,
        //    string filterColumn = null,
        //    string filterQuery = null)
        //{
        //    var accounts = await _accountRepository.GetAllAccountsAsync();

        //    return await ApiResult<Account>.CreateAsync(
        //        accounts as IQueryable<Account>, 
        //        pageIndex,
        //        pageSize,
        //        sortColumn,
        //        sortOrder,
        //        filterColumn,
        //        filterQuery);
        //}
    }
}
