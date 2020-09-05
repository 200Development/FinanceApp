using System;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountService _accountService;

        public AccountsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _accountService = new AccountService(context, configuration);
        }


        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                var sortParam = "";

                ViewData["NameSortParam"] = sortParam = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortParam = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var accountVM = await _accountService.GetAccountViewModel(1, sortParam);

                return View(accountVM);
            }
            catch (Exception e)
            {
                //TODO: Add logger
                return View("Error");
            }
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PageAccounts(string sortOrder, int page = 1)
        {
            try
            {
                var sortParam = "";

                ViewData["NameSortParam"] = sortParam = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortParam = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var accountVM = await _accountService.GetAccountViewModel(page, sortOrder);               
           

                return PartialView("_AccountsTable", accountVM);
            }
            catch (Exception e)
            {
                //Logger.Instance.Error(e);
                return View("Error");
            }
        }


    }
}
