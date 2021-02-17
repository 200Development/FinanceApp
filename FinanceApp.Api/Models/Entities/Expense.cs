using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceApp.API.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Expense 
    {
        public Expense()
        {
            // Initialize Default Values
            Name = string.Empty;
            DueDate = DateTime.MinValue;
            PayDeduction = 0.00m;
            AmountDue = 0.00m;
            PaymentFrequency = FrequencyEnum.Monthly;
            Category = null;
            CategoryId = -1;
            AccountId = -1;
            Account = null;
            DatePaid = DateTime.MinValue;
            Paid = false;
            IsBill = false;
        }


        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pay Deduction")]
        public decimal PayDeduction { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        [Required]
        [EnumDataType(typeof(FrequencyEnum))]
        [Display(Name = "Frequency")]
        public FrequencyEnum PaymentFrequency { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get;set; }
        public Category Category { get; set; }
        
        [ForeignKey("Account")]
        public long AccountId { get; set; }
        public Account Account { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Paid")]
        public DateTime DatePaid { get; set; }
        
        [Display(Name = "Paid?")]
        public bool Paid { get; set; }

        [Display(Name = "Bill?")]
        public bool IsBill { get; set; }
    }
}