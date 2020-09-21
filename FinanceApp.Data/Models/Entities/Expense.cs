using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Data.Models.Entities
{
    public class Expense
    {
        public Expense()
        {
            Due = DateTime.Today;
            Amount = decimal.Zero;
            BillId = 0;
            CreditAccountId = 0;
            IsPaid = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }
        public DateTime Due { get; set; }
        public decimal Amount { get; set; }
        public int? BillId { get; set; }
        public Bill Bill { get; set; }
        public int? CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
        public bool IsPaid { get; set; }
    }
}