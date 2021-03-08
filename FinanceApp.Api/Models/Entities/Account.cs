
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Api.Models.Entities
{
    public class Account
    {
        public Account()
        {
            // Initialize Default Values
            Name = string.Empty;
            Balance = 0.00m;
            RequiredSavings = 0.00m;  // todo: how to i dynamically set this
            BalanceLimit = 0.00m;
            IsEmergencyFund = false;
            IsCashAccount = false;
        }

        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }
        
        [Display(Name = "Required Savings")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal RequiredSavings { get; set; }

        [Display(Name = "Balance Limit")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal BalanceLimit { get; set; }
        
        [Required]
        [Display(Name = "Emergency Fund?")]
        public bool IsEmergencyFund { get; set; }

        [Required]
        [Display(Name = "Cash Account?")]
        public bool IsCashAccount { get; set; }

        [NotMapped]
        public decimal BalanceSurplus => Balance - RequiredSavings;
    }
}
