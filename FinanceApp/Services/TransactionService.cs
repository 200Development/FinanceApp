using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Base;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Enums;
using FinanceApp.Data.Models.Metrics;
using FinanceApp.Data.Repositories;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace FinanceApp.Services
{
    /// <summary>
    /// Manages all read/write to database Transaction Table
    /// </summary>
    public class TransactionService : BaseService
    {
        private readonly ExpenseService _expenseService;
        private readonly AccountService _accountService;
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(context, config, httpContextAccessor)
        {
            _expenseService = new ExpenseService(context, config, httpContextAccessor);
            _accountService = new AccountService(context, config, httpContextAccessor);
            _transactionRepository = new TransactionRepository(context, UserName);
        }

        
        public async Task<TransactionViewModel> GetViewModelAsync(string sortOrder, int page)
        {
            try
            {
                var pageSize = Convert.ToInt32(Config["defaultPageSize"]);

                var transactionVM = new TransactionViewModel();
                transactionVM.Transactions = await _transactionRepository.GetAllTransactionsAsync();
                transactionVM.Accounts = (IList<Account>) await _accountService.GetAllAccountsAsync();
                transactionVM.BillsOutstanding = await _expenseService.GetAllUnpaidExpenses();
                transactionVM.TypeSelectList = GetTransactionTypeSelectList();
                transactionVM.CategorySelectList = GetCategorySelectList();
                transactionVM.Metrics = await GetMetricsAsync();

                switch (sortOrder)
                {
                    case "name_desc":
                        transactionVM.Transactions.OrderByDescending(t => t.Payee);
                        break;
                    default:
                        transactionVM.Transactions.OrderBy(t => t.Payee);
                        break;
                }

                transactionVM.PagedTransactions = transactionVM.Transactions.ToPagedList(page, pageSize);


                return transactionVM;
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

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            try
            {
                return await _transactionRepository.GetAllTransactionsAsync();
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

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            try
            {
                return await _transactionRepository.GetTransactionByIdAsync(id);
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
        
        private async Task<TransactionMetrics> GetMetricsAsync()
        {
            try
            {
                TransactionMetrics metrics = new TransactionMetrics();

                metrics.AccountMetrics = new AccountMetrics();

                var transactions = await _transactionRepository.GetAllTransactionsAsync();
                var enumerable = transactions.ToList();

                var incomeTransactions = enumerable.Where(t => t.Type == TransactionTypesEnum.Income).ToList();
                var expenseTransactions = enumerable.Where(t => t.Type == TransactionTypesEnum.Expense).ToList();

                var expenseTransactionsByMonth = expenseTransactions.Select(t => new { t.Date.Year, t.Date.Month, t.Amount })
                    .GroupBy(x => new { x.Year, x.Month }, (key, group) => new { year = key.Year, month = key.Month, expenses = group.Sum(k => k.Amount) }).ToList();

                var incomeTransactionsByMonth = incomeTransactions.Select(t => new { t.Date.Year, t.Date.Month, t.Amount })
                    .GroupBy(x => new { x.Year, x.Month }, (key, group) => new { year = key.Year, month = key.Month, expenses = group.Sum(k => k.Amount) }).ToList();


                var expensesByMonth = new Dictionary<DateTime, decimal>();
                var mandatoryByMonth = new Dictionary<DateTime, decimal>();
                var discretionaryByMonth = new Dictionary<DateTime, decimal>();
                var incomeByMonth = new Dictionary<DateTime, decimal>();


                foreach (var transaction in expenseTransactionsByMonth)
                {
                    var date = new DateTime(transaction.year, transaction.month, 1);
                    var amount = transaction.expenses;
                    expensesByMonth.Add(date, amount);
                }

                foreach (var transaction in incomeTransactionsByMonth)
                {
                    var date = new DateTime(transaction.year, transaction.month, 1);
                    var amount = transaction.expenses;
                    incomeByMonth.Add(date, amount);
                }

               
                foreach (KeyValuePair<DateTime, decimal> transaction in expensesByMonth)
                {
                    if (expensesByMonth.ContainsKey(transaction.Key) == false)
                    {
                        expensesByMonth.Add(transaction.Key, 0m);
                    }
                }

                foreach (KeyValuePair<DateTime, decimal> transaction in incomeByMonth)
                {
                    if (incomeByMonth.ContainsKey(transaction.Key) == false)
                    {
                        incomeByMonth.Add(transaction.Key, 0m);
                    }
                }

              
                var oneYearAgo = DateTime.Today.AddYears(-1);
                var index = new DateTime(oneYearAgo.Year, oneYearAgo.Month, 1);

                for (DateTime i = index; i <= DateTime.Today; i = i.AddMonths(1))
                {
                    if (!mandatoryByMonth.ContainsKey(i))
                        mandatoryByMonth.Add(i, 0m);
                    if (!discretionaryByMonth.ContainsKey(i))
                        discretionaryByMonth.Add(i, 0m);
                    if (!expensesByMonth.ContainsKey(i))
                        expensesByMonth.Add(i, 0m);
                    if (!incomeByMonth.ContainsKey(i))
                        incomeByMonth.Add(i, 0m);
                }

                var sortedExpenses = expensesByMonth.OrderByDescending(e => e.Key);
                var sortedIncome = incomeByMonth.OrderByDescending(e => e.Key);

                metrics.MandatoryExpensesByMonth = mandatoryByMonth.Take(12).OrderBy(expense => expense.Key.Year).ThenBy(expense => expense.Key.Month).ToDictionary(expense => $"{ConvertMonthIntToString(expense.Key.Month)} {expense.Key.Year}", expense => expense.Value);
                metrics.DiscretionaryExpensesByMonth = discretionaryByMonth.Take(12).OrderBy(expense => expense.Key).ToDictionary(mandatory => $"{ConvertMonthIntToString(mandatory.Key.Month)} {mandatory.Key.Year}", mandatory => mandatory.Value);
                metrics.ExpensesByMonth = sortedExpenses.Take(12).OrderBy(expense => expense.Key).ToDictionary(disc => $"{ConvertMonthIntToString(disc.Key.Month)} {disc.Key.Year}", disc => disc.Value);
                metrics.IncomeByMonth = sortedIncome.Take(12).OrderBy(expense => expense.Key).ToDictionary(disc => $"{ConvertMonthIntToString(disc.Key.Month)} {disc.Key.Year}", disc => disc.Value);

                metrics.TransactionsByMonth = metrics.ExpensesByMonth.ToDictionary(pair => pair.Key,
                    pair => new {
                        Expenses = pair.Value,
                        Income = metrics.IncomeByMonth[pair.Key]});

            return metrics;
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
        
        public async Task<bool> CreateTransactionAsync(TransactionViewModel entity)
        {
            try
            {
                await GetUsedAccounts(entity.Transaction);

                if (entity.Transaction.SelectedExpenseId != null && entity.Transaction.SelectedExpenseId > 0)
                {
                    var setExpenseToPaid = await _expenseService.SetExpenseToPaid((int)entity.Transaction.SelectedExpenseId);
                    if (!setExpenseToPaid) return false;
                }

                await UpdateDbAccountBalances(entity.Transaction, EventArgumentEnum.Create);

                return AddTransactionToDb(entity);
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

        public async Task<bool> UpdateTransactionAsync(Transaction transaction, bool save = true)
        {
            try
            {
                await GetUsedAccounts(transaction);
                await UpdateDbAccountBalances(transaction, EventArgumentEnum.Update);

                _transactionRepository.Update(transaction);
                if (save)
                    _transactionRepository.Save();


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

        public async Task<bool> DeleteTransactionAsync(int id, bool save = true)
        {
            try
            {
                Transaction transaction = await _transactionRepository.GetTransactionByIdAsync(id);

                if (transaction.SelectedExpenseId != null && transaction.SelectedExpenseId != 0)
                {
                    var setExpenseToUnpaid = await _expenseService.SetExpenseToUnpaid((int)transaction.SelectedExpenseId);
                    if (!setExpenseToUnpaid) return false;
                }

                var updateDb = await UpdateDbAccountBalances(transaction, EventArgumentEnum.Delete);
                if (!updateDb) return false;

                _transactionRepository.Delete(transaction);
                if (save)
                    _transactionRepository.Save();


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
        
        public async Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(DateTime begin, DateTime end)
        {
            try
            {
                var expenses = await _transactionRepository.GetExpensesByCondition(t => t.Date >= begin && t.Date < end);
                return expenses.ToList();
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

        public async Task<IEnumerable<Transaction>> GetAccountsByConditionAsync(Expression<Func<Transaction, bool>> expression)
        {
            try
            {
                return await _transactionRepository.GetExpensesByCondition(expression);
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
        
        public async Task<bool> HandlePaycheckContributions(Transaction transaction)
        {
            try
            {
                if (transaction.DebitAccountId != null)
                {
                    transaction.DebitAccount = await _accountService.GetAccountByIdAsync((int)transaction.DebitAccountId);

                    var accountsWithContributions = await _accountService.GetAccountsByConditionAsync(a => a.PaycheckContribution > 0);
                    var withContributions = accountsWithContributions.ToList();

                    var totalContributions = withContributions.Sum(a => a.PaycheckContribution);
                    var incomeAfterPaycheckContributions = transaction.Amount;

                    if (totalContributions > transaction.Amount)
                    {
                        // TODO: How to handle income not enough to cover all paycheck contributions
                    }

                    foreach (var account in withContributions)
                    {
                        if (!UpdateAccountBalance(account, account.PaycheckContribution, AccountingTypes.Debit))
                            return false;
                        //if (!AddTransferToDb(transaction, account)) return false;
                        incomeAfterPaycheckContributions -= account.PaycheckContribution;
                    }

                    Logger.Instance.Calculation(
                        $"Net income of {incomeAfterPaycheckContributions} added to {transaction.DebitAccount?.Name} after {totalContributions} in paycheck contributions was paid out");
                    if (!UpdateAccountBalance(transaction.DebitAccount, incomeAfterPaycheckContributions,
                        AccountingTypes.Debit)) return false;

                    //TODO: find a better way to add remainder of income after paycheck contributions to Db
                    var incomeAfterContributions = new Transaction();
                    incomeAfterContributions.Date = transaction.Date;
                    incomeAfterContributions.Payee = $"Transfer to {transaction.DebitAccount?.Name}";
                    incomeAfterContributions.Category = transaction.Category;
                    incomeAfterContributions.Memo = transaction.Memo;
                    incomeAfterContributions.Type = transaction.Type;
                    incomeAfterContributions.DebitAccountId = transaction.DebitAccountId;
                    incomeAfterContributions.CreditAccountId = null;
                    incomeAfterContributions.Amount = incomeAfterPaycheckContributions;

                    _transactionRepository.Create(incomeAfterContributions);
                }


                _transactionRepository.Save();
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

        /// <summary>
        /// Sets the Credit & Debit Accounts from passed Id's
        /// </summary>
        /// <param name="transaction"></param>
        private async Task GetUsedAccounts(Transaction transaction)
        {
            try
            {
                if (transaction.CreditAccountId != null)
                {
                    transaction.CreditAccount = await _accountService.GetAccountByIdAsync((int)transaction.CreditAccountId);
                }

                //If income transaction, debit to pool account, else get selected debit account
                if (transaction.Type == TransactionTypesEnum.Income)
                {
                    transaction.DebitAccount = await _accountService.GetDisposableIncomeAccountAsync();
                    transaction.DebitAccountId = transaction.DebitAccount?.Id;
                }
                else if (transaction.DebitAccountId != null)
                    await _accountService.GetAccountByIdAsync((int)transaction.DebitAccountId);

            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        private async Task<bool> UpdateDbAccountBalances(Transaction transaction, EventArgumentEnum eventArgument, bool save = true)
        {
            try
            {
                switch (eventArgument)
                {
                    case EventArgumentEnum.Create:
                        {
                            if (transaction.DebitAccount != null && transaction.DebitAccount.Id != 0)
                            {
                                var originalBalance = transaction.DebitAccount.Balance; // logs beginning balance
                                transaction.DebitAccount.Balance += transaction.Amount;

                                // Update Account's required savings
                                var balanceSurplus = AccountService.UpdateBalanceSurplus(transaction.DebitAccount);
                                transaction.DebitAccount.BalanceSurplus = balanceSurplus;


                                _accountService.UpdateAccount(transaction.DebitAccount, false);
                            }

                            if (transaction.CreditAccount != null && transaction.CreditAccount.Id != 0)
                            {
                                var originalBalance = transaction.CreditAccount.Balance; // logs beginning balance
                                transaction.CreditAccount.Balance -= transaction.Amount;

                                // Update Account's required savings
                                transaction.CreditAccount.BalanceSurplus = AccountService.UpdateBalanceSurplus(transaction.CreditAccount);


                                _accountService.UpdateAccount(transaction.CreditAccount, false);
                            }

                            if (save)
                                _accountService.SaveAccount();


                            return true;
                        }
                    case EventArgumentEnum.Delete:
                    case EventArgumentEnum.Update:
                        {
                            //var originalTransaction = _db.Transactions
                            //    .AsNoTracking()
                            //    .Where(t => t.Id == transaction.Id)
                            //    .Cast<Transaction>()
                            //    .FirstOrDefault();

                            var originalTransaction = await _transactionRepository.GetTransactionByIdAsync(transaction.Id);
                            if (originalTransaction == null) return false;

                            var originalCreditAccount = await _accountService.GetAccountByIdAsync(originalTransaction.CreditAccountId ?? 0);

                            var originalDebitAccount =
                                transaction.Type == TransactionTypesEnum.Income
                                ? await _accountService.GetDisposableIncomeAccountAsync()
                                : await _accountService.GetAccountByIdAsync(originalTransaction.DebitAccountId ?? 0);

                            var originalAmount = originalTransaction.Amount;

                            // Reassign the Debit/Credit Account Id's to Transaction Model
                            transaction.CreditAccountId = originalTransaction.CreditAccountId;
                            transaction.DebitAccountId = originalTransaction.DebitAccountId;

                            if (transaction.Type == TransactionTypesEnum.Income)
                            {
                                var accounts = await _accountService.GetAllAccountsAsync();
                                var enumerable1 = accounts.ToList();
                                var enumerable = enumerable1.ToList();
                                switch (eventArgument)
                                {
                                    case EventArgumentEnum.Delete:
                                        {
                                            if (originalDebitAccount != null)
                                            {
                                                // Pool all Account balances (including deficits) 
                                                foreach (Account account in enumerable.Where(a => a.Balance != 0.0m))
                                                {
                                                    try
                                                    {
                                                        if (account.Balance > 0.0m)
                                                        {
                                                            var balance = account.Balance;
                                                            account.Balance -= balance;
                                                            originalDebitAccount.Balance += balance;
                                                        }
                                                        else if (account.Balance < 0.0m)
                                                        {
                                                            var deficit = account.Balance * -1;

                                                            account.Balance -= deficit;
                                                            originalDebitAccount.Balance -= deficit;
                                                        }

                                                        account.BalanceSurplus = AccountService.UpdateBalanceSurplus(account);
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

                                                // Subtract the amount of the income transaction from the Pool
                                                originalDebitAccount.Balance -= transaction.Amount;

                                                // If there's still a balance left, rebalance Accounts with a negative balance surplus
                                                if (originalDebitAccount.Balance > 0.0m)
                                                {
                                                    foreach (Account account in enumerable.Where(a => !a.ExcludeFromSurplus))
                                                    {
                                                        try
                                                        {
                                                            if (account.BalanceSurplus > 0.0m)
                                                            {
                                                                var balance = account.Balance;
                                                                account.Balance -= balance;
                                                                originalDebitAccount.Balance += balance;
                                                            }
                                                            else
                                                            {
                                                                var deficit = account.BalanceSurplus * -1;

                                                                if (originalDebitAccount.Balance < deficit)
                                                                {
                                                                    account.Balance += originalDebitAccount.Balance;
                                                                    originalDebitAccount.Balance -=
                                                                        originalDebitAccount.Balance;
                                                                }
                                                                else // Make account whole
                                                                {
                                                                    account.Balance += deficit;
                                                                    originalDebitAccount.Balance -= deficit;
                                                                }
                                                            }

                                                            account.BalanceSurplus = AccountService.UpdateBalanceSurplus(account);


                                                            _accountService.UpdateAccount(account, false);
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
                                                }
                                            }
                                            else
                                            {
                                                Logger.Instance.Debug("Debit account for income transaction is null");
                                            }

                                            //TODO: expose context through services to update db or use repository pattern?
                                            if (_accountService.Context.ChangeTracker.HasChanges() && save)
                                                _accountService.SaveAccount();


                                            return true;
                                        }
                                    case EventArgumentEnum.Update:
                                        {
                                            if (originalDebitAccount != null)
                                            {
                                                // Pool Account balances (including deficits)
                                                foreach (Account account in enumerable1.Where(a => a.Balance != 0.0m))
                                                {
                                                    try
                                                    {
                                                        if (account.Balance > 0.0m)
                                                        {
                                                            var balance = account.Balance;
                                                            account.Balance -= balance;
                                                            originalDebitAccount.Balance += balance;
                                                        }
                                                        else if (account.Balance < 0.0m)
                                                        {
                                                            var deficit = account.Balance * -1;

                                                            account.Balance -= deficit;
                                                            originalDebitAccount.Balance -= deficit;
                                                        }

                                                        account.BalanceSurplus = AccountService.UpdateBalanceSurplus(account);
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

                                                // Subtract difference of the updated transaction and the original transaction amount
                                                originalDebitAccount.Balance += transaction.Amount - originalAmount;

                                                // if there's a balance left, rebalance Accounts with a negative balance surplus 
                                                if (originalDebitAccount.Balance > 0.0m)
                                                {
                                                    foreach (Account account in enumerable.Where(a => !a.ExcludeFromSurplus))
                                                    {
                                                        try
                                                        {
                                                            if (account.BalanceSurplus > 0.0m)
                                                            {
                                                                var balance = account.Balance;
                                                                account.Balance -= balance;
                                                                originalDebitAccount.Balance += balance;
                                                            }
                                                            else
                                                            {
                                                                var deficit = account.BalanceSurplus * -1;

                                                                if (originalDebitAccount.Balance < deficit)
                                                                {
                                                                    account.Balance += originalDebitAccount.Balance;
                                                                    originalDebitAccount.Balance -=
                                                                        originalDebitAccount.Balance;
                                                                }
                                                                else // Make account whole
                                                                {
                                                                    account.Balance += deficit;
                                                                    originalDebitAccount.Balance -= deficit;
                                                                }
                                                            }

                                                            account.BalanceSurplus = AccountService.UpdateBalanceSurplus(account);


                                                            _accountService.UpdateAccount(account, false);
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
                                                }
                                            }
                                            else
                                            {
                                                Logger.Instance.Debug("Debit account for income transaction is null");
                                            }


                                            if (_accountService.Context.ChangeTracker.HasChanges() && save)
                                                _accountService.SaveAccount();


                                            return true;
                                        }
                                    default:
                                        throw new NotImplementedException();
                                }
                            }

                            switch (eventArgument)
                            {
                                case EventArgumentEnum.Delete:
                                    {
                                        if (originalDebitAccount != null)
                                        {
                                            originalDebitAccount.Balance -= transaction.Amount;

                                            // Update Account's required savings

                                            originalDebitAccount.BalanceSurplus = AccountService.UpdateBalanceSurplus(originalDebitAccount);
                                            _accountService.UpdateAccount(originalDebitAccount, false);
                                        }

                                        if (originalCreditAccount != null)
                                        {
                                            originalCreditAccount.Balance += transaction.Amount;

                                            // Update Account's required savings

                                            originalCreditAccount.BalanceSurplus = AccountService.UpdateBalanceSurplus(originalCreditAccount);
                                            _accountService.UpdateAccount(originalCreditAccount, false);
                                        }

                                        if (save)
                                            _accountService.SaveAccount();


                                        return true;
                                    }
                                case EventArgumentEnum.Update:
                                    {
                                        var amountDifference = transaction.Amount - originalAmount;
                                        if (originalDebitAccount != null)
                                        {
                                            originalDebitAccount.Balance += amountDifference;
                                            originalDebitAccount.BalanceSurplus = AccountService.UpdateBalanceSurplus(originalDebitAccount);
                                            _accountService.UpdateAccount(originalDebitAccount, false);
                                        }

                                        if (originalCreditAccount != null)
                                        {
                                            originalCreditAccount.Balance -= amountDifference;
                                            originalCreditAccount.BalanceSurplus = AccountService.UpdateBalanceSurplus(originalCreditAccount);
                                            _accountService.UpdateAccount(originalCreditAccount, false);
                                        }

                                        if (save)
                                            _accountService.SaveAccount();


                                        return true;
                                    }
                                default:
                                    throw new NotImplementedException();
                            }
                        }
                    default:
                        throw new NotImplementedException($"{eventArgument} is not an accepted type for TransactionController.UpdateDbAccountBalances method");
                }
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

        // TODO: is this mapping needed??
        private bool AddTransactionToDb(TransactionViewModel entity)
        {
            try
            {
                var newTransaction = new Transaction();
                //newTransaction.UserId = entity.Transaction.UserId;
                newTransaction.Date = entity.Transaction.Date;
                newTransaction.Payee = entity.Transaction.SelectedExpenseId != null && entity.Transaction.SelectedExpenseId > 0
                    ? _expenseService.GetExpenseByIdAsync((int)entity.Transaction.SelectedExpenseId).Result.Name
                    : entity.Transaction.Payee;
                newTransaction.Category = entity.Transaction.Category;
                //newTransaction.Memo = entity.Transaction.Memo;
                newTransaction.Type = entity.Transaction.Type;
                newTransaction.DebitAccountId = entity.Transaction.DebitAccountId;
                newTransaction.CreditAccountId = entity.Transaction.CreditAccountId;
                newTransaction.Amount = entity.Transaction.Amount;
                if (entity.Transaction.SelectedExpenseId != null && entity.Transaction.SelectedExpenseId > 0)
                    newTransaction.SelectedExpenseId = entity.Transaction.SelectedExpenseId;
                
                _transactionRepository.CreateTransaction(newTransaction);
                _transactionRepository.Save();


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

        private bool UpdateAccountBalance(Account account, decimal amount, AccountingTypes type)
        {
            try
            {
                switch (type)
                {
                    case AccountingTypes.Debit:
                        account.Balance += amount;
                        break;
                    case AccountingTypes.Credit:
                        account.Balance -= amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                _accountService.UpdateAccount(account);
                _accountService.SaveAccount();


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
                    throw new NotImplementedException();
            }
        }

        private SelectList GetTransactionTypeSelectList()
        {
            var items = new List<SelectListItem>();

            foreach (var frequency in Enum.GetValues(typeof(TransactionTypesEnum)))
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

        private SelectList GetCategorySelectList()
        {
            var items = new List<SelectListItem>();

            foreach (var frequency in Enum.GetValues(typeof(TransactionTypesEnum)))
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

    public enum AccountingTypes
    {
        Debit,
        Credit
    }
}
