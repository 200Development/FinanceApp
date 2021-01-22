using System.ComponentModel.DataAnnotations;

namespace FinanceApp.API.Enums
{
    public enum FrequencyEnum
    {
        Annually = 0,
        [Display(Name = "Bi-Annually")]
        BiAnnually = 1,
        Quarterly = 2,
        Monthly = 3,
        [Display(Name = "Bi-Monthly")]
        BiMonthly = 4,
        [Display(Name = "Bi-Weekly")]
        BiWeekly = 5,
        Weekly = 6,
        Daily = 7
    }
}