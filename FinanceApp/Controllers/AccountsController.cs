using System;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Services;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace FinanceApp.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly AccountService _accountService;

        public AccountsController(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = new AccountService(context, config, httpContextAccessor);
        }


        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                try
                {
                    await _accountService.CheckAndCreatePoolAccountAsync();
                    await _accountService.CheckAndCreateEmergencyFundAsync();
                    await _accountService.CheckAndCreateAddNewAccountAsync();
                }
                catch (Exception e)
                {
                   Logger.Instance.Error(e);
                }

                ViewData["NameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var accountVM = await _accountService.GetViewModelAsync(1, sortOrder);

                return View(accountVM);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
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

                var accountVM = await _accountService.GetViewModelAsync(page, sortOrder);               
           

                return PartialView("_AccountsTable", accountVM);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        public IActionResult Create(AccountViewModel accountVm)
        {
            try
            {
                if (!_accountService.CreateAccount(accountVm.Account)) return View("Error");

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }
    }
}
