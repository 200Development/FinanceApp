using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Enums;
using FinanceApp.Data.Models.Metrics;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Services
{
    public class DashboardService
    {
        private readonly AccountService _accountService;
        private readonly BillService _billService;
        private readonly TransactionService _transactionService;
        private readonly ExpenseService _expenseService;
        private static decimal _minimumMonthlyExpenses = 0.00m;
        private const decimal GrossIncome = 100000;
        private const int Age = 45;

        public DashboardService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = new AccountService(context, config, httpContextAccessor);
            _billService = new BillService(context, config, httpContextAccessor);
            _transactionService = new TransactionService(context, config, httpContextAccessor);
            _expenseService = new ExpenseService(context, config, httpContextAccessor);
        }


        public async Task<DashboardViewModel> GetViewModel()
        {
            DashboardViewModel vm = new DashboardViewModel();
            vm.Accounts = await _accountService.GetAllAccountsAsync();
            vm.Metrics = await RefreshStaticMetrics();

            return vm;
        }

        private async Task<DashboardMetrics> RefreshStaticMetrics()
        {
            var metrics = new DashboardMetrics();

            try
            {
                metrics.DisposableIncome = await GetDisposableIncome();
                metrics.TargetedNetWorth = GetTargetedNetWorth();
                metrics.SavingsRate = await GetSavingsRate();
                metrics.BudgetRuleExpense = GetBudgetRuleExpenses();
                metrics.BudgetRuleSavings = GetBudgetRuleSavings();
                metrics.BudgetRuleDiscretionary = GetBudgetRuleDiscretionary();
                metrics.MinimumMonthlyExpenses = _minimumMonthlyExpenses = await GetMinimumMonthlyExpenses();
                metrics.CashFlowByMonth = await GetCashFlowByMonth();
                metrics.EmergencyFundRatio = await GetEmergencyFundRatio();
                metrics.DueBeforeNextPayPeriod = await GetDueBeforeNextPayPeriod();
                metrics.CashBalance = await GetCashBalance();


                return metrics;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
        }

        private async Task<decimal> GetDisposableIncome()
        {
            try
            {
                var poolAccount = await _accountService.GetPoolAccountAsync();
                return poolAccount.Balance;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return decimal.MinValue;
            }
        }

        private static decimal GetTargetedNetWorth()
        {
            try
            {
                // TODO: Add income & age
                return Age * (GrossIncome / 10);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<decimal> GetSavingsRate()
        {
            try
            {
                var emergencyAccount = await _accountService.GetEmergencyFundAccountAsync();
                return emergencyAccount.Balance / GrossIncome;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static decimal GetBudgetRuleExpenses()
        {
            try
            {
                return GrossIncome * .50m;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static decimal GetBudgetRuleSavings()
        {
            try
            {
                return GrossIncome * .20m;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static decimal GetBudgetRuleDiscretionary()
        {
            try
            {
                return GrossIncome * .30m;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<decimal> GetMinimumMonthlyExpenses()
        {
            try
            {
                var totalMonthlyExpense = 0.00m;
                var bills = await _billService.GetAllBillsAsync();

                foreach (Bill bill in bills)
                {
                    switch (bill.PaymentFrequency)
                    {
                        case FrequencyEnum.Daily:
                            totalMonthlyExpense += bill.AmountDue * 365 / 12;
                            break;
                        case FrequencyEnum.Weekly:
                            totalMonthlyExpense += bill.AmountDue * 52 / 12;
                            break;
                        case FrequencyEnum.BiWeekly:
                            totalMonthlyExpense += bill.AmountDue * 26 / 12;
                            break;
                        case FrequencyEnum.Monthly:
                            totalMonthlyExpense += bill.AmountDue;
                            break;
                        case FrequencyEnum.SemiMonthly:
                            totalMonthlyExpense += bill.AmountDue * 2;
                            break;
                        case FrequencyEnum.Quarterly:
                            totalMonthlyExpense += bill.AmountDue / 3;
                            break;
                        case FrequencyEnum.SemiAnnually:
                            totalMonthlyExpense += bill.AmountDue / 6;
                            break;
                        case FrequencyEnum.Annually:
                            totalMonthlyExpense += bill.AmountDue / 12;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return Math.Round(totalMonthlyExpense, 2);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return decimal.MinValue;
            }
        }

        private async Task<Dictionary<string, decimal>> GetCashFlowByMonth()
        {
            try
            {
                var allTransactions = await _transactionService.GetAllTransactionsAsync();

                var transactionsByMonth = allTransactions
                    .Select(t => new { t.Date.Year, t.Date.Month, t.Amount, t.Type })
                    .GroupBy(t => new { t.Year, t.Month },
                        (key, group) => new { year = key.Year, month = key.Month, cashflow = group.Sum(k => k.Type == TransactionTypesEnum.Expense ? -1 * k.Amount : k.Amount) }).ToList();

                var transactionsByMonthDict = new Dictionary<DateTime, decimal>();

                foreach (var item in transactionsByMonth)
                {
                    var date = new DateTime(item.year, item.month, 1);
                    var amount = item.cashflow;
                    transactionsByMonthDict.Add(date, amount);
                }


                foreach (var pair in transactionsByMonthDict.Where(pair => transactionsByMonthDict.ContainsKey(pair.Key) == false))
                {
                    transactionsByMonthDict.Add(pair.Key, 0m);
                }


                var oneYearAgo = DateTime.Today.AddYears(-1);
                var index = new DateTime(oneYearAgo.Year, oneYearAgo.Month, 1);

                for (DateTime i = index; i <= DateTime.Today; i = i.AddMonths(1))
                {
                    if (!transactionsByMonthDict.ContainsKey(i))
                        transactionsByMonthDict.Add(i, 0m);
                }

                return transactionsByMonthDict.Take(12).OrderBy(t => t.Key.Year).ThenBy(t => t.Key.Month)
                    .ToDictionary(t => $"{ConvertMonthIntToString(t.Key.Month)} {t.Key.Year}", t => t.Value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<decimal> GetEmergencyFundRatio()
        {
            try
            {
                var emergencyFund = await _accountService.GetEmergencyFundAccountAsync();
                return emergencyFund.Balance / _minimumMonthlyExpenses;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<decimal> GetDueBeforeNextPayPeriod()
        {
            try
            {
                var nextPayday = new DateTime(2020, 5, 1);
                var unpaidExpenses = await _expenseService.GetAllUnpaidExpenses();

                return unpaidExpenses.Where(e => e.Due < nextPayday).Sum(e => e.Amount);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<decimal> GetCashBalance()
        {
            var accountBalanceSum = _accountService.GetAllAccountsAsync().Result.Sum(a => a.Balance);
            var poolAccount = await _accountService.GetPoolAccountAsync();
            accountBalanceSum += poolAccount.Balance;


            return accountBalanceSum;
        }

        private string ConvertMonthIntToString(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
