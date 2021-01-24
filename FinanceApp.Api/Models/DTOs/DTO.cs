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
        public List<Expense> ExpensesDueBeforeNextPayDay { get; set; }
        public IList<Expense> ExpensesDueThisMonth { get; set; }
        public DashboardDTO DashboardDto { get; set; }
        public decimal? SumOfAccountBalances { get; set; }
        public decimal? IncomeThisMonth { get; set; }
        public decimal? CostOfBillsPerPayPeriod { get; set; }
        public decimal? MonthlyCostOfBills { get; set; }
        public decimal? MonthlyCostOfExpenses { get; set; }
        public decimal? TotalSurplus { get; set; }
        public decimal? CostOfExpensesPerPayPeriod { get; set; }
        public decimal? TotalExpensesDueBeforeNextPayDay { get; set; }
        public decimal? RequiredSavings { get; set; }
        public decimal? CurrentSavings { get; set; }
        public decimal? RemainingIncomeThisMonth { get; set; }
        public decimal? DisposableIncome { get; set; }
        public decimal? CostOfDiscretionaryExpensesThisMonth { get; set; }
        public decimal? CostOfMandatoryExpensesThisMonth { get; set; }
        public decimal? CashBalance { get; set; }
        public decimal? AccountingBalance { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
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
