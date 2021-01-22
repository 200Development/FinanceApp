using System.Collections.Generic;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Models.Metrics;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FinanceApp.ViewModels
{
    public class BillViewModel
    {
        public BillViewModel()
        {
            Bill = new Bill();
            Bills = new List<Bill>();
            Expenses = new List<Expense>();
            Metrics = new BillMetrics();
        }

        public Bill Bill { get; set; }
        public IList<Bill> Bills { get; set; }
        public IPagedList<Bill> PagedBills { get; set; }
        public IList<Expense> Expenses { get; set; }
        public IPagedList<Expense> PagedExpenses { get; set; }
        public IList<Account> Accounts { get; set; }
        public SelectList PaymentFrequencySelectList { get; set; }
        public BillMetrics Metrics { get; set; }
    }
}