using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Models.Entities
{
    public class Expense
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Paid")]
        public DateTime DatePaid { get; set; }

        [Required, DataType(DataType.Currency)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        public int? BillId { get; set; }

        public Bill Bill { get; set; }

        public bool Paid { get; set; }
    }
}