using System.Collections.Generic;
using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class DTO
    {
        public IList<AccountDTO> AccountDtos { get; set; }
        public IList<BillDTO> BillDtos { get; set; }
        public decimal? SumOfAccountBalances { get; set; }
        public decimal? CostOfBillsPerPayPeriod { get; set; }
        public decimal? MonthlyCostOfBills { get; set; }
        public decimal? TotalSurplus { get; set; }
    }

    public class AccountDTO
    {
        public Account Account { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public decimal BillSum { get; set; }
        public decimal? PayDeduction { get; set; }
        public decimal? PaycheckPercentage { get; set; }
        public decimal? ExpensesBeforeNextPaycheck { get; set; }
        public decimal? BalanceSurplus { get; set; }
        public decimal? RequiredSavings { get; set; }
    }

    public class BillDTO
    {
        public Bill Bill { get; set; }
        public decimal? PayDeduction { get; set; }
        public decimal? PaycheckPercentage { get; set; }
        public decimal? MonthlyPercentage { get; set; }
        public decimal? RequiredSavings { get; set; }
    }
}
