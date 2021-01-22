using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Enums;
using FinanceApp.Data.Models.Metrics;
using FinanceApp.Data.Repositories;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace FinanceApp.Services
{
    /// <summary>
    /// Manages all read/write to database Bills Table
    /// </summary>
    public class BillService : BaseService
    {
        private readonly BillRepository _billRepository;
        private readonly AccountService _accountService;
        private readonly ExpenseService _expenseService;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public BillService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(context, config, httpContextAccessor)
        {
            _billRepository = new BillRepository(context, UserName);
            _accountService = new AccountService(context, config, httpContextAccessor);
            _expenseService = new ExpenseService(context, config, httpContextAccessor);
            _context = context;
            _configuration = config;
        }


        public async Task<BillViewModel> GetViewModelAsync(int page, string sortOrder)
        {
            try
            {
                var pageSize = Convert.ToInt32(_configuration["defaultPageSize"]);

                var billVM = new BillViewModel();
                billVM.Bills = (IList<Bill>)await _billRepository.GetAllBillsAsync();
                billVM.Accounts = (IList<Account>)await _accountService.GetAllAccountsAsync();
                billVM.Expenses = (IList<Expense>)await _expenseService.GetAllExpensesAsync();
                billVM.Metrics = await GetBillMetricsAsync();
                billVM.PaymentFrequencySelectList = GetPaymentFrequencySelectList();

                switch (sortOrder)
                {
                    case "name_desc":
                        billVM.Bills.OrderByDescending(a => a.Name);
                        break;
                    case "Balance":
                        billVM.Bills.OrderBy(a => a.AmountDue);
                        break;
                    case "balance_desc":
                        billVM.Bills.OrderByDescending(a => a.AmountDue);
                        break;
                    default:
                        billVM.Bills.OrderBy(a => a.Name);
                        break;
                }

                billVM.PagedBills = billVM.Bills.ToPagedList(page, pageSize);
                billVM.PagedExpenses = billVM.Expenses.ToPagedList(page, pageSize);


                return billVM;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            try
            {
                return await _billRepository.GetAllBillsAsync();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }
        
        public async Task<IEnumerable<Bill>> GetBillsByConditionAsync(Expression<Func<Bill, bool>> expression)
        {
            try
            {
                return await _billRepository.GetBillsByCondition(expression);
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }
        
        public async Task<Bill> GetBillByIdAsync(int id)
        {
            try
            {
                return await _billRepository.GetBillByIdAsync(id);
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Returns summation of bills within the date range of the begin and end parameters  
        /// </summary>
        /// <param name="begin">start date of the date range</param>
        /// <param name="end">end date of the date range</param>
        /// <param name="onlyMandatory">sumates only mandatory expenses</param>
        /// <returns></returns>
        public async Task<decimal> ExpensesByDateRangeAsync(DateTime begin, DateTime end, bool onlyMandatory = false)
        {
            try
            {
                var bills = await _billRepository.GetAllBillsAsync();
                var expenses = 0m;

                foreach (var bill in bills)
                {
                    //if (bill.DueDate.Date < beginDate) continue;

                    var frequency = bill.PaymentFrequency;
                    var dueDate = bill.DueDate;
                    var newDueDate = dueDate;

                    //TODO: Fix semi-monthly bills being added 3 times (31st, 16th, 1st)
                    while (newDueDate >= begin)
                    {
                        switch (frequency)
                        {
                            case FrequencyEnum.Daily:
                                newDueDate = newDueDate.AddDays(-1);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.Weekly:
                                newDueDate = newDueDate.AddDays(-7);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.BiWeekly:
                                newDueDate = newDueDate.AddDays(-14);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.Monthly:
                                newDueDate = newDueDate.AddMonths(-1);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.SemiMonthly:
                                newDueDate = newDueDate.AddDays(-15);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.Quarterly:
                                newDueDate = newDueDate.AddMonths(-3);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.SemiAnnually:
                                newDueDate = newDueDate.AddMonths(-6);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            case FrequencyEnum.Annually:
                                newDueDate = newDueDate.AddYears(-1);
                                Logger.Instance.Calculation($"{bill.Name} new due date is {newDueDate:d}");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        // adds expense only if the bill due date falls within the date range
                        if (newDueDate < begin || newDueDate >= end) continue;
                        expenses += bill.AmountDue;
                        Logger.Instance.Calculation($"Expenses: {expenses} added to {bill.Name}.AmountDue");
                    }
                }
                return expenses;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return 0.0m;
            }
        }

        /// <summary>
        /// Updates database Bills.DueDate if the previous due date has passed
        /// </summary>
        public async Task UpdateBillDueDatesAsync(bool save = true)
        {
            try
            {
                Logger.Instance.Calculation($"UpdateBillDueDates");
                var bills = await _billRepository.GetAllBillsAsync();
                var beginDate = DateTime.Today;

                foreach (var bill in bills)
                {
                    if (bill.DueDate.Date > beginDate) continue;

                    var frequency = bill.PaymentFrequency;
                    var dueDate = bill.DueDate;
                    var newDueDate = dueDate;

                    /* Updates bill due date to the current due date
                       while loop handles due date updates, regardless of how out of date they are */
                    while (newDueDate < beginDate)
                    {
                        switch (frequency)
                        {
                            case FrequencyEnum.Daily:
                                newDueDate = newDueDate.AddDays(1);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.Weekly:
                                newDueDate = newDueDate.AddDays(7);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.BiWeekly:
                                newDueDate = newDueDate.AddDays(14);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.Monthly:
                                newDueDate = newDueDate.AddMonths(1);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.SemiMonthly:
                                newDueDate = newDueDate.AddDays(15);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.Quarterly:
                                newDueDate = newDueDate.AddMonths(3);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.SemiAnnually:
                                newDueDate = newDueDate.AddMonths(6);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            case FrequencyEnum.Annually:
                                newDueDate = newDueDate.AddYears(1);
                                Logger.Instance.Calculation($"New due date {newDueDate:d}");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    bill.DueDate = newDueDate;
                    _billRepository.Update(bill);

                    var addBillToExpenses = await AddBillToExpensesAsync(bill, save);
                    if (!addBillToExpenses) return;
                }

                if (save)
                    _billRepository.Save();
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
        }

        /// <summary>
        /// Returns Dictionary with due dates for all bills for calculating all expenses within a timeframe.  Used for calculating future savings
        /// </summary>
        /// <param name="billsDictionary"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> UpdateBillDueDatesAsync(Dictionary<string, string> billsDictionary)
        {
            try
            {
                var bills = await _billRepository.GetAllBillsAsync();
                var beginDate = Convert.ToDateTime(billsDictionary["currentDate"]);

                foreach (var bill in bills)
                {
                    if (bill.DueDate.Date > beginDate) continue;

                    var frequency = bill.PaymentFrequency;
                    var dueDate = bill.DueDate;
                    var newDueDate = dueDate;

                    while (newDueDate < beginDate)
                    {
                        switch (frequency)
                        {
                            case FrequencyEnum.Daily:
                                newDueDate = newDueDate.AddDays(1);
                                break;
                            case FrequencyEnum.Weekly:
                                newDueDate = newDueDate.AddDays(7);
                                break;
                            case FrequencyEnum.BiWeekly:
                                newDueDate = newDueDate.AddDays(14);
                                break;
                            case FrequencyEnum.Monthly:
                                newDueDate = newDueDate.AddMonths(1);
                                break;
                            case FrequencyEnum.SemiMonthly:
                                newDueDate = newDueDate.AddDays(15);
                                break;
                            case FrequencyEnum.Quarterly:
                                newDueDate = newDueDate.AddMonths(3);
                                break;
                            case FrequencyEnum.SemiAnnually:
                                newDueDate = newDueDate.AddMonths(6);
                                break;
                            case FrequencyEnum.Annually:
                                newDueDate = newDueDate.AddYears(1);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        billsDictionary[bill.Name] = newDueDate.ToShortDateString();
                    }
                }
                return billsDictionary;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
        }

        public async Task<Dictionary<string, string>> UpdateTotalCostsAsync(Dictionary<string, string> billsDictionary)
        {
            try
            {
                var bills = await _billRepository.GetAllBillsAsync();
                var currentDate = Convert.ToDateTime(billsDictionary["currentDate"]);
                var endDate = Convert.ToDateTime(billsDictionary["endDate"]);
                var expenses = 0.0m;
                billsDictionary["periodCosts"] = "0";

                foreach (var bill in billsDictionary)
                {
                    if (bill.Key == "currentDate" || bill.Key == "endDate" || bill.Key == "periodCosts" ||
                        bill.Key == "totalSavings" || bill.Key == "totalCosts") continue;

                    var dueDate = Convert.ToDateTime(bill.Value);
                    if (!(dueDate >= currentDate && dueDate <= endDate)) continue;

                    expenses += bills.Where(b => b.Name == bill.Key).Select(b => b.AmountDue).FirstOrDefault();
                }

                var billCosts = Convert.ToDecimal(billsDictionary["totalCosts"]);
                billsDictionary["totalCosts"] = (expenses + billCosts).ToString(CultureInfo.InvariantCulture);
                billsDictionary["periodCosts"] = expenses.ToString(CultureInfo.InvariantCulture);

                return billsDictionary;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
        }

        public async Task<bool> CreateBillAsync(Bill bill, bool save = true)
        {
            try
            {

                if (!CreateBill(bill, save)) return false;

                var addBillToExpenses = await AddBillToExpensesAsync(bill, save);
                if (addBillToExpenses) return false;

                var updateResult = await UpdateAccountPaycheckContributionAsync();
                if (!updateResult) return false;

                Logger.Instance.DataFlow($"Saved changes to DB");


                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }

        public bool UpdateBill(Bill bill, bool save = true)
        {
            try
            {
                _billRepository.Update(bill);
                if (save)
                    _billRepository.Save();

                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }

        public async Task<bool> DeleteBillAsync(int billId, bool save = true)
        {
            try
            {
                IEnumerable<Expense> expenses = await _expenseService.GetAllExpensesForBillAsync(billId);
                foreach (Expense expense in expenses)
                {
                    _expenseService.DeleteExpense(expense, false);
                }

                if (_context.ChangeTracker.HasChanges() && save)
                    _expenseService.SaveExpense();

                Bill bill = await _billRepository.GetBillByIdAsync(billId);

                _billRepository.Delete(bill);
                if (save)
                    _billRepository.Save();


                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }

        private bool CreateBill(Bill bill, bool save = true)
        {
            try
            {
                _billRepository.CreateBill(bill);
                if (save)
                    _billRepository.Save();

                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }
        
        private async Task<BillMetrics> GetBillMetricsAsync()
        {
            List<Bill> bills = (List<Bill>)await _billRepository.GetAllBillsAsync();
            BillMetrics metrics = new BillMetrics();

            try
            {
                metrics.LargestBalance = bills.Max(b => b.AmountDue);
                metrics.SmallestBalance = bills.Min(b => b.AmountDue);
                metrics.TotalBalance = bills.Sum(b => b.AmountDue);
                metrics.AverageBalance = bills.Sum(b => b.AmountDue) / bills.Count;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return new BillMetrics();
            }


            return metrics;
        }

        //TODO: Update to only update effected Accounts
        private async Task<bool> UpdateAccountPaycheckContributionAsync(bool save = true)
        {
            try
            {
                var updatedAccounts = new List<Account>();
                var paycheckContributionsDict = await CalculationsService.GetPaycheckContributionsDict(_accountService, this);

                // Update paycheck contribution for all accounts
                foreach (var paycheckContribution in paycheckContributionsDict)
                {
                    try
                    {
                        var account = updatedAccounts.Find(a => string.Equals(a.Name, paycheckContribution.Key, StringComparison.CurrentCultureIgnoreCase));
                        var accountIndex = -1;

                        if (account == null)
                        {
                            account = new Account { Name = paycheckContribution.Key };
                        }
                        else
                            accountIndex = updatedAccounts.FindIndex(a => string.Equals(a.Name, paycheckContribution.Key, StringComparison.CurrentCultureIgnoreCase));

                        if (!(paycheckContribution.Value >= account.PaycheckContribution)) continue;


                        if (accountIndex >= 0)
                            updatedAccounts[accountIndex].PaycheckContribution = paycheckContribution.Value;
                        else
                        {
                            account.PaycheckContribution = paycheckContribution.Value;
                            updatedAccounts.Add(account);
                        }


                        // iterate through all updated accounts and set state to modified to save to database
                        var accounts = (List<Account>)await _accountService.GetAllAccountsAsync();

                        foreach (var updatedAccount in updatedAccounts)
                        {
                            try
                            {

                                account = accounts.Find(a => string.Equals(a.Name, updatedAccount.Name, StringComparison.CurrentCultureIgnoreCase));

                                // shouldn't ever be null since updatedAccounts comes from Accounts in DB
                                account.PaycheckContribution = updatedAccount.PaycheckContribution;
                                account.RequiredSavings = updatedAccount.RequiredSavings;

                                var requiredSurplus = account.Balance - account.RequiredSavings;

                                if (requiredSurplus <= 0)
                                    account.BalanceSurplus = requiredSurplus;
                                else
                                {
                                    if (account.Balance - account.BalanceLimit <= 0)
                                        account.BalanceSurplus = 0;
                                    else
                                        account.BalanceSurplus = account.Balance - account.BalanceLimit;
                                }


                                _accountService.UpdateAccount(account, false);
                            }
                            catch (Exception e)
                            {
                                Logger.Instance.Error(e);
                            }
                        }

                        // save changes to the database
                        if (_context.ChangeTracker.HasChanges() && save)
                            _accountService.SaveAccount();

                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Error(e);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return false;
            }
        }

        private async Task<bool> AddBillToExpensesAsync(Bill bill, bool save = true)
        {
            try
            {
                var expenseExists = await ExpenseExistsAsync(bill);
                if (expenseExists)
                {
                    Logger.Instance.DataFlow($"{bill.Name} due {bill.DueDate} already exists in Expense DB table");
                    return false;
                }

                var newExpense = new Expense();
                newExpense.Name = bill.Name;
                newExpense.BillId = bill.Id;
                newExpense.Amount = bill.AmountDue;
                newExpense.Due = bill.DueDate;
                newExpense.CreditAccountId = bill.AccountId;
                newExpense.IsPaid = false;

                _expenseService.CreateExpense(newExpense);
                if (save)
                    _expenseService.SaveExpense();


                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return false;
            }
        }

        private async Task<bool> ExpenseExistsAsync(Bill bill)
        {
            try
            {
                var response = await _expenseService.GetExpensesByConditionAsync(e => e.BillId == bill.Id && e.Due == bill.DueDate);

                return response.Any(e => e.Amount == bill.AmountDue);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw new Exception("Error thrown checking for existing Expense", e);
            }
        }

        private SelectList GetPaymentFrequencySelectList()
        {
            var items = new List<SelectListItem>();

            foreach (var frequency in Enum.GetValues(typeof(FrequencyEnum)))
            {
                var item = new SelectListItem
                {
                    Text = Enum.GetName(typeof(FrequencyEnum), frequency),
                    Value = frequency.ToString()
                };

                items.Add(item);
            }


            return new SelectList(items);
        }
    }
}
