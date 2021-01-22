using System;
using System.Collections.Generic;
using System.Globalization;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Enums;
using FinanceApp.Data.Models.Metrics;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FinanceApp.ViewModels
{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            Transaction = new Transaction();
            Transactions = new List<Transaction>();
            Metrics = new TransactionMetrics();
            AutoTransferPaycheckContributions = false;
            Date = DateTime.Today.ToString("d", CultureInfo.CurrentCulture);

            try
            {
              //  Accounts = new AccountService().GetAllAccountsForTransactions();
               // GetFilterOptions();
            }
            catch (Exception e)
            {
                //Accounts = new AccountManager().GetAllAccounts();
                Logger.Instance.Error(e);
            }
        }

        public Transaction Transaction { get; set; }
        public TransactionMetrics Metrics { get; set; }
        public SelectList TypeSelectList { get; set; }
        public SelectList CategorySelectList { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IPagedList<Transaction> PagedTransactions { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Expense> BillsOutstanding { get; set; }
        public bool AutoTransferPaycheckContributions { get; }
        public TransactionTypesEnum Type { get; set; }
        public CategoriesEnum Category { get; set; }
        public string Date { get; set; }
        public bool IsBill { get; set; }
        
        
        // Using Class instead of Enum to allow custom display names
        private List<TransactionFilterOptions> GetFilterOptions()
        {
            var options = new List<TransactionFilterOptions>();
            options.Add(new TransactionFilterOptions { Name = "All", DisplayName = "All Transactions" });
            options.Add(new TransactionFilterOptions { Name = "Income", DisplayName = "Income" });
            options.Add(new TransactionFilterOptions { Name = "Expense", DisplayName = "Expenses" });
            options.Add(new TransactionFilterOptions { Name = "Transfers", DisplayName = "Transfers" });
            options.Add(new TransactionFilterOptions { Name = "Rebalancing", DisplayName = "Rebalancing Transactions" });
            options.Add(new TransactionFilterOptions { Name = "Paycheck", DisplayName = "Paycheck Contributions" });


            return options;
        }
    }

    public class TransactionFilterOptions
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}