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
    public class TransactionsController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly ExpenseService _expenseService;


        public TransactionsController(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _transactionService = new TransactionService(context, config, httpContextAccessor);
            _expenseService = new ExpenseService(context, config, httpContextAccessor);
        }


        [System.Web.Mvc.HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                ViewData["TransactionPayeeSortParam"] = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";

                var transactionVM = await _transactionService.GetViewModelAsync(sortOrder, 1);

                return View(transactionVM);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TransactionViewModel transactionVM)
        {
            try
            {
                switch (transactionVM.Transaction.Type)
                {
                    case TransactionTypesEnum.Expense:
                    {
                            if (transactionVM.IsBill)
                            {
                                if (transactionVM.Transaction?.SelectedExpenseId != null)
                                {
                                    var payee = await _expenseService.GetExpenseByIdAsync(
                                        (int)transactionVM.Transaction?.SelectedExpenseId);
                                    transactionVM.Transaction.Payee = payee.Name ?? string.Empty;
                                }
                                else if (transactionVM.Transaction != null)
                                    transactionVM.Transaction.Payee = string.Empty;

                                ModelState["Transaction.Payee"].Errors.Clear();
                            }
                            else
                            {
                                await _transactionService.CreateTransactionAsync(transactionVM);
                            }


                           // if (!ModelState.IsValid) return View("Error");
                          //  await _transactionService.CreateTransactionAsync(transactionVM);
                            break;
                        }
                    case TransactionTypesEnum.Income:
                        {
                            if (!transactionVM.AutoTransferPaycheckContributions)
                                await _transactionService.CreateTransactionAsync(transactionVM);
                            else
                            {
                                if (!await _transactionService.HandlePaycheckContributions(transactionVM.Transaction))
                                    return View("Error");
                            }

                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditTransaction(Transaction transaction)
        {
            try
            {
                return Json(await _transactionService.UpdateTransactionAsync(transaction) ? "Success" : "Failed");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTransaction(Transaction transaction)
        {
            try
            {
                return transaction.Id > 0
                    ? Json(await _transactionService.DeleteTransactionAsync(transaction.Id) ? "Success" : "Failed")
                    : Json("ID is invalid");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return Json("Error");
            }
        }

        [HttpGet]
        public ActionResult PageTransactions(string sortOrder, int page = 1)
        {
            try
            {
                var transactionVM = _transactionService.GetViewModelAsync(sortOrder, page);


                return PartialView("_TransactionsTable");
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return View("Error");
            }
        }
    }
}
