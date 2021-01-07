using System.ComponentModel.DataAnnotations;
using FinanceApp.API.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Income
    {
        [Key] 
        public long Id { get; set; }

        public string UserId { get; set; }

        public string Payer { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [EnumDataType(typeof(FrequencyEnum))]
        [Required, Display(Name = "Pay Frequency")]
        public FrequencyEnum PaymentFrequency { get; set; }

        public int? FirstMonthlyPayDay { get; set; }

        public int? SecondMonthlyPayDay { get; set; }
    }
}