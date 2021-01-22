using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceApp.Data.Models.Enums;

namespace FinanceApp.Data.Models.Entities
{
    public class Transaction 
    {
        public Transaction()
        {
            Date = DateTime.Today;
            Payee = string.Empty;
            Memo = string.Empty;
            CreditAccountId = 0;
            DebitAccountId = 0;
            SelectedExpenseId = 0;
            Amount = decimal.Zero;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
      
        public string Payee { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        
        public string Memo { get; set; }
        
        public TransactionTypesEnum Type { get; set; }
        
        public CategoriesEnum Category { get; set; }
        
        public int? CreditAccountId { get; set; }
        
        [Display(Name = "From")]
        [ForeignKey("CreditAccountId")]
        public Account CreditAccount { get; set; }
        
        public int? DebitAccountId { get; set; }
        
        [Display(Name = "To")]
        [ForeignKey("DebitAccountId")]
        public Account DebitAccount { get; set; }

        public int? SelectedExpenseId { get; set; }

        [ForeignKey("SelectedExpenseId")]
        public Expense SelectedExpense { get; set; }
    }
}