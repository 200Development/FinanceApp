using System.ComponentModel.DataAnnotations;

namespace FinanceApp.API.Enums
{
    public enum FrequencyEnum
    {
        Annually = 0,
        [Display(Name = "Twice a Year")]
        BiAnnually = 1,
        Quarterly = 2,
        Monthly = 3,
        [Display(Name = "Twice a Month")]
        BiMonthly = 4,
        [Display(Name = "Every 2 Weeks")]
        BiWeekly = 5,
        Weekly = 6,
        Daily = 7
    }
}