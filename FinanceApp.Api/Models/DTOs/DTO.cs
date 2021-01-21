using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class DTO
    {
        public IList<AccountDTO> AccountDtos { get; set; }
        public IList<BillDTO> BillDtos { get; set; }
        public IList<ExpenseDTO> ExpenseDtos { get; set; }
        public IList<TransactionDTO> TransactionDtos { get; set; }
        public DashboardDTO DashboardDto { get; set; }
        public decimal? SumOfAccountBalances { get; set; }
        public decimal? CostOfBillsPerPayPeriod { get; set; }
        public decimal? MonthlyCostOfBills { get; set; }
        public decimal? MonthlyCostOfExpenses { get; set; }
        public decimal? TotalSurplus { get; set; }
        public decimal? CostOfExpensesPerPayPeriod { get; set; }
    }

    public class DashboardDTO
    {
        public IList<Expense> ExpensesDueThisMonth { get; set; }
        public IList<Expense> ExpensesDueBeforeNextPayday { get; set; }
        public IList<Account> Accounts { get; set; }
        public Income Income { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public decimal? OutstandingMonthlyIncome { get; set; }
        public decimal? DisposableCash { get; set; }
    }
}
