
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Models.Entities
{
    public class Account
    {
         public Account()
        {
          Balance = 0.00m;
          PaycheckContribution = 0.00m;
          SuggestedPaycheckContribution = 0.00m;
          RequiredSavings = 0.00m;  // todo: how to i dynamically set this
          BalanceLimit = 0.00m;
          IsEmergencyFund = false;
          IsCashAccount = false;
        }

        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true), Display(Name = "Paycheck Contribution")]
        public decimal PaycheckContribution { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true), Display(Name = "Suggested Paycheck Contribution")]
        public decimal SuggestedPaycheckContribution { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true), Display(Name = "Required Savings")]
        public decimal RequiredSavings { get; set; }
        [Display(Name = "Balance Limit"), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal BalanceLimit { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true), Display(Name = "Surplus/Deficit")]
        public decimal BalanceSurplus => _ = this.Balance - this.RequiredSavings;

        [Required]
        [Display(Name = "Emergency Fund")]
        public bool IsEmergencyFund { get; set; }

        /// <summary>
        /// User created account.  All other Accounts are for tracking expenses
        /// </summary>
        [Required]
        [Display(Name = "Cash Account")]
        public bool IsCashAccount { get; set; }
    }
}
