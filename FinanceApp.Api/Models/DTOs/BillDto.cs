using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class BillDto : Bill
    {
        public int? FrequencyId { get; set; }
        public int? CategoryId { get; set; }
    }
}