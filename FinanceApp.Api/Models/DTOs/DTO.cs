using System.Collections.Generic;

namespace FinanceApp.Api.Models.DTOs
{
    public class DTO
    {
        public IList<AccountDTO> AccountDtos { get; set; }
        public IList<BillDTO> BillDtos { get; set; }
        public IList<ExpenseDTO> ExpenseDtos { get; set; }
        public IList<TransactionDTO> TransactionDtos { get; set; }
        public decimal? SumOfAccountBalances { get; set; }
        public decimal? CostOfBillsPerPayPeriod { get; set; }
        public decimal? MonthlyCostOfBills { get; set; }
        public decimal? MonthlyCostOfExpenses { get; set; }
        public decimal? TotalSurplus { get; set; }
        public decimal? CostOfExpensesPerPayPeriod { get; set; }
    }
}
