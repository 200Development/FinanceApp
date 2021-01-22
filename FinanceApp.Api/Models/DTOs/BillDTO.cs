using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class BillDTO
    {
        public Bill Bill { get; set; }
        public decimal? PayDeduction { get; set; }
        public decimal? PaycheckPercentage { get; set; }
        public decimal? MonthlyPercentage { get; set; }
        public decimal? RequiredSavings { get; set; }
    }
}