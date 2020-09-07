using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace FinanceApp.Controllers
{
    [Authorize]
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
                ViewData["NameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var accountVM = await _accountService.GetAccountViewModel(1, sortOrder);

                return View(accountVM);
            }
            catch (Exception e)
            {
                //TODO: Add logger
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PageAccounts(string sortOrder, int page = 1)
        {
            try
            {
                ViewData["NameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var accountVM = await _accountService.GetAccountViewModel(page, sortOrder);               
           

                return PartialView("_AccountsTable", accountVM);
            }
            catch (Exception e)
            {
             //   Logger.Instance.Error(e);
                return View("Error");
            }
        }

        public IActionResult Create(AccountViewModel accountVm)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                accountVm.Account.UserId = userId;

                if (!_accountService.Create(accountVm.Account)) return View("Error");


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return View("Error");
            }
        }
    }
}
