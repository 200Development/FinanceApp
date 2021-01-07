using System;
using System.ComponentModel.DataAnnotations;
using FinanceApp.Api.Enums;
using FinanceApp.API.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Bill 
    {
        public Bill()
        {
            DueDate = DateTime.Today;    
        }


        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public decimal PayDeduction { get; set; }

        [Required, DataType(DataType.Currency)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        [EnumDataType(typeof(FrequencyEnum))]
        [Required, Display(Name = "Frequency")]
        public FrequencyEnum PaymentFrequency { get; set; }

        [EnumDataType(typeof(CategoriesEnum))]
        [Required, Display(Name = "Category")]
        public CategoriesEnum Category { get; set; }

        public long AccountId { get; set; }

        public Account Account { get; set; }
    }
}