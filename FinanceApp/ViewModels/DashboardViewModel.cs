using System.Collections.Generic;
using System.Linq;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Metrics;

namespace FinanceApp.ViewModels
{
    public class DashboardViewModel 
    {
        public DashboardViewModel()
        {
            Accounts = new List<Account>();
            //Bills = new List<Bill>();
            //Expenses = new List<Expense>();
            //Transactions = new List<Transaction>();
            Metrics = new DashboardMetrics();
            //TimePeriodMetrics = new TimeValueOfMoneyMetrics();
        }
        

        public IEnumerable<Account> Accounts { get; set; }
        //public List<Bill> Bills { get; set; }
        //public List<Expense> Expenses { get; set; }
        //public List<Transaction> Transactions { get; set; }
        public DashboardMetrics Metrics { get; set; }
        //public TimeValueOfMoneyMetrics TimePeriodMetrics { get; set; }
    }
}