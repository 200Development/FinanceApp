using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Services;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;
        private readonly AccountService _accountService;


        public DashboardController(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardService = new DashboardService(context, config, httpContextAccessor);
            _accountService = new AccountService(context, config, httpContextAccessor);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DashboardViewModel dashboardVM = await _dashboardService.GetViewModel();

            return View(dashboardVM);
        }
    }
}
