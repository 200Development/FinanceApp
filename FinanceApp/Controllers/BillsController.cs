using System;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Enums;
using FinanceApp.Services;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly AccountService _accountService;
        private readonly BillService _billService;
        private readonly ExpenseService _expenseService;
        private readonly TransactionService _transactionService;


        public BillsController(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = new AccountService(context, config, httpContextAccessor);
            _billService = new BillService(context, config, httpContextAccessor);
            _expenseService = new ExpenseService(context, config, httpContextAccessor);
            _transactionService = new TransactionService(context, config, httpContextAccessor);
        }


        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                ViewData["NameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var billVM = await _billService.GetViewModelAsync(1, sortOrder);


                return View(billVM);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PageExpenses(string sortOrder, int page = 1)
        {

            try
            {
                ViewData["ExpenseNameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["ExpenseBalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var billVM = await _billService.GetViewModelAsync(1, sortOrder);


                return PartialView("_ExpensesTable", billVM);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PageBills(string sortOrder, int page = 1)
        {

            try
            {
                ViewData["BillNameSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                ViewData["BillBalanceSortParam"] = sortOrder == "Balance" ? "balance_desc" : "Balance";

                var billVM = await _billService.GetViewModelAsync(1, sortOrder);


                return PartialView("_ExpensesTable");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Create(BillWithAccount vm)
        {
            try
            {
                if (vm.FrequencyId > 0) vm.Frequency = (FrequencyEnum)vm.FrequencyId;


                var bill = MapToBill(vm);
                var billCreated = false;
                if (vm.AccountId != 0)
                {
                    billCreated = await _billService.CreateBillAsync(bill);
                    return Json(!billCreated ? "Error" : "Success");
                }


                var account = MapToAccount(vm);
                _accountService.CreateAccount(account);
                bill.AccountId = account.Id;

                billCreated = await _billService.CreateBillAsync(bill);
                return Json(!billCreated ? "Error" : "Success");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpPost]
        public JsonResult EditBill(Bill bill)
        {
            try
            {
                return Json(_billService.UpdateBill(bill) ? "Success" : "Failed");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBill(Bill bill)
        {
            try
            {
                return bill.Id > 0 ? Json(await _billService.DeleteBillAsync(bill.Id) ? "Success" : "Failed") : Json("ID is invalid");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetExpenseToPaid(Expense expense)
        {
            try
            {
                if (expense.Id >= 1)
                    return await _expenseService.SetExpenseToPaid(expense.Id) ? Json("Success") : Json("Failed");


                Logger.Instance.Info("Invalid Expense Id");
                return Json("Invalid Expense Id");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetExpenseToPaidWithTransaction(Expense expense)
        {
            try
            {
                if (expense.Id < 1)
                {
                    Logger.Instance.Info("Invalid Expense Id");
                    return Json("Invalid Expense Id");
                }

                if (expense.CreditAccountId < 1)
                {
                    Logger.Instance.Info("Invalid Credit Account Id");
                    return Json("Invalid Credit Account Id");
                }

                var transactionVM = new TransactionViewModel();
                var transaction = new Transaction();
                transaction.Type = TransactionTypesEnum.Expense;
                transaction.Category = CategoriesEnum.ManualPayment;
                transaction.Amount = expense.Amount;
                transaction.CreditAccountId = expense.CreditAccountId;
                transaction.SelectedExpenseId = expense.Id;
                transaction.Date = expense.Due;
                transaction.Payee = "Manual Expense Payment";

                transactionVM.Transaction = transaction;


                return await _transactionService.CreateTransactionAsync(transactionVM) ? Json("Success") : Json("Failed");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }
        
        // TODO: move to service layer
        private Bill MapToBill(BillWithAccount vm)
        {
            var bill = new Bill();

            bill.Name = vm.Name;
            bill.AmountDue = vm.AmountDue;
            bill.PaymentFrequency = vm.Frequency;
            bill.DueDate = vm.DueDate;
            if (vm.AccountId != null) bill.AccountId = (int)vm.AccountId;


            return bill;
        }

        private Account MapToAccount(BillWithAccount vm)
        {
            var account = new Account();

            account.Name = vm.AccountName;
            account.Balance = vm.AccountBalance;
            account.PaycheckContribution = vm.AccountPaycheckContribution;


            return account;
        }
    }

    public class BillWithAccount
    {
        public string Name { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public int? FrequencyId { get; set; }
        public FrequencyEnum Frequency { get; set; }
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal AccountPaycheckContribution { get; set; }
    }
}
