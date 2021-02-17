using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceApp.Api.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            // Initialize Default Values
            Payee = string.Empty;
            Date = DateTime.MinValue;
            Amount = 0.00m;
            Category = null;
            CategoryId = -1;
            Type = TransactionTypesEnum.Expense;
        }

        [Key]
        public long Id { get; set; }
        
        public string UserId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Payee { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
       
        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [Required]
        public TransactionTypesEnum Type { get; set; }
    }
}
