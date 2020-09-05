using System.Collections.Generic;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Metrics;
using X.PagedList;

namespace FinanceApp.ViewModels
{
    public class AccountViewModel : IAccountViewModel
    {
        public AccountViewModel()
        {
            Account = new Account();
            Metrics = new AccountMetrics();
        }

        public Account Account { get; set; }
        public IList<Account> Accounts { get; set; }
        public IPagedList<Account> PagedAccounts { get; set; }
        public AccountMetrics Metrics { get; set; }
    }

    public interface IAccountViewModel
    {
        Account Account { get; set; }
        IList<Account> Accounts { get; set; }
        IPagedList<Account> PagedAccounts { get; set; }
        AccountMetrics Metrics { get; set; }
    }
}
