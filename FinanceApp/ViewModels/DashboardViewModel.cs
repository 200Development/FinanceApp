using System.Collections.Generic;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Metrics;

namespace FinanceApp.ViewModels
{
    public class DashboardViewModel 
    {
        public DashboardViewModel()
        {
            Accounts = new List<Account>();
            Metrics = new DashboardMetrics();
        }
        

        public IEnumerable<Account> Accounts { get; set; }
        public Account DisposableIncomeAccount { get; set; }
        public DashboardMetrics Metrics { get; set; }
    }
}