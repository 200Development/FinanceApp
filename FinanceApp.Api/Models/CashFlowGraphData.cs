
namespace FinanceApp.Api.Models
{
    public class CashFlowGraph
    {
        public string Date { get; set; }
        public CashFlowData DataPoints { get; set; }
    }
    public class CashFlowData
    {
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal CashFlow { get; set; }
    }
}
