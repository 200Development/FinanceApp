using System.Collections.Generic;
using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class AccountDto
    {
        public Account Account { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public decimal BillSum { get; set; }
        public decimal? PayDeduction { get; set; }
    }
}
