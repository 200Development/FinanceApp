using System;

namespace FinanceApp.Api.Models
{
    public class AmortizedExpenses
    {
        public string Name { get; set; }    
        public DateTime Due { get; set; }
        public decimal Amount { get; set; }
        public int PaydaysUntilDue { get; set; }
        public decimal RequiredSavings { get; set; }
    }
}
