﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FinanceApp.API.Enums;
using FinanceApp.Api.Models.Entities;
using Microsoft.VisualBasic;
using X.PagedList;

namespace FinanceApp.API.Services
{
    public static class CalculationsService
    {
        //TODO: Refactor CalculationsService to remove any references to the database context

        static CalculationsService()
        {
        }

        /// <summary>
        /// Returns the last day of the month for the provided date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime date)
        {
            try
            {
                var lastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                return lastDay;
            }
            catch (Exception e)
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// Returns the first day of the month for the provided year and month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(int year, int month)
        {
            //TODO: change to take datetime as parameter
            try
            {
                var firstDay = new DateTime(year, month, 1);
                return firstDay;
            }
            catch (Exception e)
            {
                return new DateTime();
            }
        }

        //public static async Task<decimal> FutureValue(DateTime futureDate, decimal? netPay)
        //{
        //    try
        //    {
        //        Logger.Instance.Calculation($"FutureValue");
        //        var payperiods = PayPeriodsTilDue(futureDate);
        //        var date = DateTime.Today;
        //        var billsFromDb = await _billService.GetAllBillsAsync();

        //        var bills = new Dictionary<string, string>
        //        {
        //            {"currentDate", DateTime.Today.ToShortDateString()},
        //            {"endDate", DateTime.Today.Day <= 14
        //                ? new DateTime(date.Year, date.Month, 15).ToShortDateString()
        //                : new DateTime(date.Year, date.Month, LastDayOfMonth(date).Day).ToShortDateString()
        //            },
        //            {"periodCosts", "0"},
        //            {"totalCosts", "0"},
        //            {"totalSavings", "0"}
        //        };

        //        foreach (var bill in billsFromDb)
        //        {
        //            bills.Add(bill.Name, bill.DueDate.ToShortDateString());
        //        }

        //        for (var i = 0; i < payperiods; i++)
        //        {

        //            bills = billManager.UpdateBillDueDates(bills);
        //            bills = billManager.UpdateTotalCosts(bills);
        //            SetCurrentAndEndDate(bills);
        //            decimal? savings = Convert.ToDecimal(bills["totalSavings"]);
        //            var periodCosts = Convert.ToDecimal(bills["periodCosts"]);

        //            savings += netPay - periodCosts;
        //            bills["totalSavings"] = savings.ToString();
        //        }
        //        //var cost = Convert.ToDecimal(bills["periodCosts"]);
        //        var save = Convert.ToDecimal(bills["totalSavings"]);
        //        Logger.Instance.Calculation($"FutureValue({futureDate:d}, {netPay}) returned {save}");

        //        return save;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public static decimal DiscretionarySpendingByDateRange(DateTime begin, DateTime end)
        //{
        //    try
        //    {
        //        Logger.Instance.Calculation($"DiscretionarySpendingByDateRange");
        //        var transactionManager = new TransactionService();
        //        var transactions = transactionManager.GetTransactionsBetweenDates(begin, end);
        //        var billManager = new BillService();
        //        var bills = billManager.GetAllBills();
        //        var isBill = false;
        //        var ret = 0m;

        //        foreach (var transaction in transactions)
        //        {
        //            foreach (var bill in bills)
        //            {
        //                // If the bill name matches the transaction payee, count the transaction as a bill (mandatory expense)
        //                if (bill.Name.Equals(transaction.Payee))
        //                {
        //                    isBill = true;
        //                    Logger.Instance.Calculation($"{transaction.Amount}: {transaction.Payee} transaction is a bill");
        //                }
        //                else
        //                    Logger.Instance.Calculation($"{transaction.Amount}: {transaction.Payee} transaction is not a bill");
        //            }

        //            // If transaction is not a bill, add to discretionary spending total
        //            if (isBill) continue;

        //            ret += transaction.Amount;
        //            Logger.Instance.Calculation($"{transaction.Amount} added to discretionary spending total");
        //        }
        //        Logger.Instance.Calculation($"{ret} total discretionary spending from {begin:d} to {end:d}");
        //        return ret;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.Error(e);
        //        return 0.0m;
        //    }
        //}

        //public static decimal DailyInterest(Loan loan)
        //{
        //    try
        //    {
        //        Logger.Instance.Calculation($"DailyInterest");
        //        var dailyInterestRate = (loan.APR / 100) / (decimal)364.25;
        //        var dailyInterest = dailyInterestRate * loan.OutstandingBalance;
        //        Logger.Instance.Calculation($"{loan.Name} loan daily interest = {dailyInterest} (dailyInterestRate {dailyInterestRate} * outstandingBalance {loan.OutstandingBalance})");
        //        return dailyInterest;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.Error(e);
        //        return 0.0m;
        //    }
        //}

        //public static decimal MonthlyInterest(Loan loan)
        //{
        //    try
        //    {
        //        Logger.Instance.Calculation($"MonthlyInterest");
        //        var monthlyInterestRate = (loan.APR / 100) / 12;
        //        var monthlyInterest = monthlyInterestRate * loan.OutstandingBalance;
        //        Logger.Instance.Calculation($"{loan.Name} loan monthly interest = {monthlyInterest} (monthlyInterestRate {monthlyInterestRate} * outstandingBalance {loan.OutstandingBalance})");
        //        return monthlyInterest;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.Error(e);
        //        return 0.0m;
        //    }
        //}

        //public static Dictionary<string, decimal> GetAccountRequiredSavingsDict()
        //{
        //    try
        //    {
        //        var savingsAccountBalances = new Dictionary<string, decimal>();
        //        ExpenseManager expenseManager = new ExpenseManager();
        //        AccountManager accountManager = new AccountManager();
        //        BillManager billManager = new BillManager();
        //        List<Expense> unpaidExpenses = expenseManager.GetAllUnpaidExpenses();

        //        //foreach (var bill in _db.Bills.ToList())
        //        foreach (var expense in unpaidExpenses)
        //        {
        //            try
        //            {
        //                var bill = billManager.GetBill(expense.BillId);
        //                if (bill == null)
        //                {
        //                    Logger.Instance.Debug($"No Bill found WHERE expense.BillId = {expense.BillId}");
        //                    Logger.Instance.DataFlow($"No Bill found WHERE expense.BillId = {expense.BillId}");
        //                    continue;
        //                }

        //                bill.Account = accountManager.GetAccount(bill.AccountId);
        //                if (bill.Account == null) continue;
        //                var billTotal = expense.Amount; // Use info from Expense and not Bill to account for when the current Bill.Amount differs from past amounts
        //                var dueDate = expense.Due;
        //                var payPeriodsLeft = PayPeriodsTilDue(dueDate);
        //                var savePerPaycheck = 0.0m;
        //                var save = 0.0m;

        //                // Calculate how much to save each pay period
        //                if (dueDate > DateTime.Today)
        //                {
        //                    switch (bill.PaymentFrequency)
        //                    {
        //                        case FrequencyEnum.Annually:
        //                            savePerPaycheck = billTotal / 24;
        //                            if (payPeriodsLeft <= 24)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.SemiAnnually:
        //                            savePerPaycheck = billTotal / 12;
        //                            if (payPeriodsLeft <= 12)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.Quarterly:
        //                            savePerPaycheck = billTotal / 6;
        //                            if (payPeriodsLeft <= 6)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.SemiMonthly:
        //                            savePerPaycheck = billTotal / 4;
        //                            if (payPeriodsLeft <= 4)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.Monthly:
        //                            savePerPaycheck = billTotal / 2;
        //                            if (payPeriodsLeft <= 2)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.BiWeekly:
        //                            savePerPaycheck = billTotal;
        //                            if (payPeriodsLeft > 1)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        case FrequencyEnum.Weekly:
        //                            savePerPaycheck = billTotal * 2;
        //                            if (payPeriodsLeft > 1)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                        default:
        //                            savePerPaycheck = billTotal / 2;
        //                            if (payPeriodsLeft <= 2)
        //                            {
        //                                save = billTotal - payPeriodsLeft * savePerPaycheck;
        //                                save = Math.Round(save, 2);
        //                            }
        //                            Logger.Instance.Calculation($"{expense.Name} save/paycheck = {Math.Round(savePerPaycheck, 2)} to {bill.Account.Name} account");
        //                            break;
        //                    }
        //                }
        //                else
        //                    save = expense.Amount;

        //                Logger.Instance.Calculation($"{bill.Account.Name} - [{Math.Round(billTotal, 2)}] [{bill.DueDate:d}] [{payPeriodsLeft}(ppl)] [{Math.Round(savePerPaycheck, 2)}(spp)] [{Math.Round(save, 2)}(req save)]");

        //                if (savingsAccountBalances.ContainsKey(bill.Account.Name))
        //                    savingsAccountBalances[bill.Account.Name] += save;
        //                else
        //                    savingsAccountBalances.Add(bill.Account.Name, save);
        //            }
        //            catch (Exception e)
        //            {
        //                Logger.Instance.Error(e);
        //                throw;
        //            }
        //        }


        //        return savingsAccountBalances;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.Error(e);
        //        throw;
        //    }
        //}

        //TODO: Refactor to remove magic string
        public static Dictionary<long?, decimal> GetPayDeductionDict(List<Account> accounts, List<Bill> bills, string returnType = "account")
        {
            try
            {
                var accountContribution = new Dictionary<long?, decimal>();
                var billContribution = new Dictionary<long?, decimal>();


                //Zeros out all accounts req paycheck contributions
                foreach (var account in accounts)
                {
                    account.PaycheckContribution = 0.0m;
                }

                //Zeros out all bills req paycheck contributions
                foreach (var bill in bills)
                {
                    bill.PayDeduction = 0.0m;
                }

                // update suggested paycheck contributions for bills
                foreach (var bill in bills)
                {
                    var billTotal = bill.AmountDue;

                    // get the account assigned to the bill
                    bill.Account = accounts.FirstOrDefault(a => a.Id == bill.AccountId);
                    if (bill.Account != null && bill.Account.ExcludeFromSurplus) continue;

                    //TODO: Needs to account for all pay frequencies
                    //TODO: Suggested contribution assumes payday twice a month.  need to update to include other options
                    if (bill.Account == null) continue;
                    var contribution = 0.0m;
                    
                    switch (bill.PaymentFrequency)
                    {
                        case FrequencyEnum.Annually:
                            contribution = billTotal / 24;
                            break;
                        case FrequencyEnum.BiAnnually:
                            contribution = billTotal / 12;
                            break;
                        case FrequencyEnum.Quarterly:
                            contribution = billTotal / 6;
                            break;
                        case FrequencyEnum.BiMonthly: // every 2 months
                            contribution = billTotal / 4;
                            break;
                        case FrequencyEnum.Monthly:
                            contribution = billTotal / 2;
                            break;
                        case FrequencyEnum.Weekly:
                            contribution = billTotal * 2;
                            break;
                        case FrequencyEnum.BiWeekly:
                            contribution = billTotal;
                            break;
                        case FrequencyEnum.Daily:
                            break;
                        default:
                            contribution = billTotal / 2;
                            break;
                    }

                    if (accountContribution.ContainsKey(bill.AccountId!))
                        accountContribution[bill.AccountId] += contribution;
                    else
                        accountContribution.Add(bill.AccountId, contribution);

                    if (billContribution.ContainsKey(bill.Id))
                        billContribution[bill.Id] += contribution;
                    else
                        billContribution.Add(bill.Id, contribution);
                }

                return returnType.Equals("account") ? accountContribution : billContribution;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static Dictionary<long?, decimal> GetPayDeductionDict(List<Account> accounts, List<Expense> expenses, string returnType)
        {
            try
            {
                var accountContribution = new Dictionary<long?, decimal>();
                var billContribution = new Dictionary<long?, decimal>();


                //Zeros out all accounts req paycheck contributions
                foreach (var account in accounts)
                {
                    account.PaycheckContribution = 0.0m;
                }

                //Zeros out all bills req paycheck contributions
                foreach (var expense in expenses)
                {
                    expense.PayDeduction = 0.0m;
                }

                // update suggested paycheck contributions for bills
                foreach (var expense in expenses)
                {
                    var billTotal = expense.AmountDue;

                    // get the account assigned to the bill
                    expense.Account = accounts.FirstOrDefault(a => a.Id == expense.AccountId);
                    if (expense.Account != null && expense.Account.ExcludeFromSurplus) continue;

                    //TODO: Needs to account for all pay frequencies
                    //TODO: Suggested contribution assumes payday twice a month.  need to update to include other options
                    if (expense.Account == null) continue;
                    var contribution = 0.0m;

                    switch (expense.PaymentFrequency)
                    {
                        case FrequencyEnum.Annually:
                            contribution = billTotal / 24;
                            break;
                        case FrequencyEnum.BiAnnually:
                            contribution = billTotal / 12;
                            break;
                        case FrequencyEnum.Quarterly:
                            contribution = billTotal / 6;
                            break;
                        case FrequencyEnum.BiMonthly: // every 2 months
                            contribution = billTotal / 4;
                            break;
                        case FrequencyEnum.Monthly:
                            contribution = billTotal / 2;
                            break;
                        case FrequencyEnum.Weekly:
                            contribution = billTotal * 2;
                            break;
                        case FrequencyEnum.BiWeekly:
                            contribution = billTotal;
                            break;
                        case FrequencyEnum.Daily:
                            break;
                        default:
                            contribution = billTotal / 2;
                            break;
                    }

                    if (accountContribution.ContainsKey(expense.AccountId!))
                        accountContribution[expense.AccountId] += contribution;
                    else
                        accountContribution.Add(expense.AccountId, contribution);

                    if (billContribution.ContainsKey(expense.Id))
                        billContribution[expense.Id] += contribution;
                    else
                        billContribution.Add(expense.Id, contribution);
                }

                return returnType.Equals("account") ? accountContribution : billContribution;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Returns how many times the user will get paid before a due date
        /// </summary>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        private static int PayPeriodsTilDue(DateTime? dueDate)
        {
            try
            {
                var payPeriods = 0;
                var today = DateTime.Today;
                var month = DateTime.Today.Month;
                var year = DateTime.Today.Year;
                var firstDayOfMonth = new DateTime(year, month, 1);
                var firstPaycheckDate = new DateTime(year, month, 15);

                while (dueDate > today)
                {
                    if (today >= firstDayOfMonth && today <= firstPaycheckDate) // 1st - 15th of the month
                    {
                        payPeriods += 1;
                        today = firstPaycheckDate.AddDays(1);
                    }
                    else // 16th through the last day of the month
                    {
                        payPeriods += 1;
                        firstDayOfMonth = firstDayOfMonth.AddMonths(1);
                        firstPaycheckDate = firstPaycheckDate.AddMonths(1);
                        today = firstDayOfMonth;
                    }
                }
                return payPeriods - 1 < 0 ? 0 : payPeriods - 1;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns Dictionary (string, string) with current and end (next pay period) dates set
        /// </summary>
        /// <param name="billsDictionary"></param>
        private static void SetCurrentAndEndDate(IDictionary<string, string> billsDictionary)
        {
            try
            {
                var currentDate = Convert.ToDateTime(billsDictionary["currentDate"]);
                var endDate = Convert.ToDateTime(billsDictionary["endDate"]);

                if (Convert.ToDateTime(billsDictionary["currentDate"]).Day <= 14)
                {
                    billsDictionary["currentDate"] = new DateTime(currentDate.Year, currentDate.Month, 16).ToShortDateString();
                    currentDate = Convert.ToDateTime(billsDictionary["currentDate"]);
                    endDate = new DateTime(currentDate.Year, currentDate.Month, LastDayOfMonth(currentDate).Day);
                    billsDictionary["endDate"] = endDate.ToShortDateString();
                }
                else
                {
                    billsDictionary["currentDate"] =
                        FirstDayOfMonth(currentDate.AddMonths(1).Year, currentDate.AddMonths(1).Month)
                            .ToString(CultureInfo.InvariantCulture);
                    currentDate = Convert.ToDateTime(billsDictionary["currentDate"]);
                    endDate = new DateTime(currentDate.Year, currentDate.Month, 15);
                    billsDictionary["endDate"] = endDate.ToShortDateString();
                }
            }
            catch (Exception e)
            {
            }
        }

        public static decimal GetPaycheckPercentage(Dictionary<long?, decimal> payDeductionDict, decimal payDeduction)
        {
            var totalPaycheckContributions = payDeductionDict.Values.Sum();
            var paycheckContribution = 0.0m;

            if (totalPaycheckContributions != 0)
                paycheckContribution = (payDeduction / totalPaycheckContributions);


            return paycheckContribution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payDeductionDict"></param>
        /// <param name="bills"></param>
        /// <returns></returns>
        public static Dictionary<long?, decimal> GetAccountRequiredSavingsDict(Dictionary<long?, decimal> payDeductionDict,
            IList<Bill> bills)
        {
            try
            {
                var requiredSavingsDict = new Dictionary<long?, decimal>();

                if (!bills.Any())
                    return requiredSavingsDict;

                foreach (var dict in payDeductionDict)
                {
                    var accountBills = bills.Where(b => b.AccountId == dict.Key).ToList();

                    if (!accountBills.Any()) continue;

                    foreach (var bill in accountBills)
                    {
                        var paydaysLeft = GetPaydaysUntilDue(bill.DueDate);
                        var pdTest = PayPeriodsTilDue(bill.DueDate);


                        if (requiredSavingsDict.ContainsKey(bill.AccountId!)) // null msg suppressed with '!'.  bill requires accountId to be in dictionary
                            requiredSavingsDict[bill.AccountId] += bill.AmountDue - dict.Value * paydaysLeft;
                        else
                            requiredSavingsDict.Add(bill.AccountId, bill.AmountDue - dict.Value * paydaysLeft);
                    }
                }

                return requiredSavingsDict;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static decimal GetBillRequiredSavings(Dictionary<long?, decimal> payDeductionDict, Bill bill)
        {
            var payDeduction = payDeductionDict[bill.Id];
            var paydaysUntilDue = GetPaydaysUntilDue(bill.DueDate);

            return bill.AmountDue - payDeduction * paydaysUntilDue;
        }

        private static int GetPaydaysUntilDue(DateTime dueDate)
        {
            var payFrequency = GetPayFrequency();
            var nextPayday = GetNextPayday();
            var paychecks = 0;


            while (dueDate >= nextPayday)
            {
                paychecks++;
                nextPayday = GetNextPayday(payFrequency, nextPayday);
            }

            return paychecks;
        }

        private static FrequencyEnum GetPayFrequency()
        {
            return FrequencyEnum.BiWeekly;
        }

        private static DateTime GetNextPayday()
        {
            //TODO: just a filler for now
            return DateTime.Today.AddDays(10);
        }

        private static DateTime GetNextPayday(FrequencyEnum frequency, DateTime lastPayday)
        {
            return frequency switch
            {
                FrequencyEnum.Annually => lastPayday.AddYears(1),
                FrequencyEnum.BiAnnually => lastPayday.AddMonths(6),
                FrequencyEnum.Quarterly => lastPayday.AddMonths(3),
                FrequencyEnum.Monthly => lastPayday.AddMonths(1),
                FrequencyEnum.BiMonthly => lastPayday.AddDays(14), // should this be 2 specific days every month?
                FrequencyEnum.BiWeekly => lastPayday.AddDays(14),
                FrequencyEnum.Weekly => lastPayday.AddDays(7),
                FrequencyEnum.Daily => lastPayday.AddDays(1),
                _ => throw new ArgumentOutOfRangeException(nameof(frequency), frequency, "Frequency is not an option"),
            };
        }

        public static decimal? GetExpenseRequiredSavings(Dictionary<long?, decimal> payDeductionDict, Expense expense)
        {
            var payDeduction = payDeductionDict[expense.Id];
            var paydaysUntilDue = GetPaydaysUntilDue(expense.DueDate);

            return expense.AmountDue - payDeduction * paydaysUntilDue;
        }
    }
}