using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Api.Models.Entities
{
    public class Income
    {
        public Income()
        {
            Payer = string.Empty;
            NextPayday = DateTime.MinValue;
            Amount = 0.00m;
            PaymentFrequency = null;
            UpdateNextPayday();
        }

        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Payer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NextPayday { get; private set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey("Frequency")]
        [Display(Name = "Pay Frequency")]
        public int PaymentFrequencyId { get; set; }
        public Freqency PaymentFrequency { get; set; }

        public int? FirstMonthlyPayDay { get; set; }

        public int? SecondMonthlyPayDay { get; set; }

        public void UpdateNextPayday()
        {
            var today = DateTime.Now;

            if (this.NextPayday >= today)
            {
                switch (PaymentFrequency.Name.ToLower())
                {
                    case "annually":
                        this.NextPayday = this.NextPayday.AddYears(1);
                        break;
                    case "biannually":
                        this.NextPayday = this.NextPayday.AddMonths(6);
                        break;
                    case "quarterly":
                        this.NextPayday = this.NextPayday.AddMonths(3);
                        break;
                    case "monthly":
                        this.NextPayday = this.NextPayday.AddMonths(1);
                        break;
                    case "bimonthly":
                        if (today.Day >= this.SecondMonthlyPayDay)
                        {
                            var month = today.AddMonths(1).Month;
                            var firstMonthlyPayDay = this.FirstMonthlyPayDay;
                            if (firstMonthlyPayDay != null)
                            {
                                var day = firstMonthlyPayDay.Value;
                                var year = today.Month == 12 ? today.Year + 1 : today.Year;
                                this.NextPayday = new DateTime(year, month, day);
                            }
                            else
                            {
                                throw new NullReferenceException($"{nameof(this.FirstMonthlyPayDay)} is Null");
                            }
                        }
                        else if (today.Day < FirstMonthlyPayDay)
                        {
                            this.NextPayday = new DateTime(today.Year, today.Month, this.FirstMonthlyPayDay.Value);
                        }
                        else if (today.Day >= this.FirstMonthlyPayDay && today.Day < this.SecondMonthlyPayDay)
                        {
                            this.NextPayday = new DateTime(today.Year, today.Month, this.SecondMonthlyPayDay.Value);
                        }
                        break;
                    case "biweekly":
                        if (today >= this.NextPayday)
                        {
                            var payday = this.NextPayday;
                            while (payday <= today)
                            {
                                payday = payday.AddDays(14);
                                if (payday.DayOfWeek == DayOfWeek.Saturday)
                                    payday = payday.AddDays(-1);
                                if (payday.DayOfWeek == DayOfWeek.Sunday)
                                    payday = payday.AddDays(-2);

                            }

                            this.NextPayday = payday;
                        }
                        break;
                    case "weekly":
                        if (today >= this.NextPayday)
                        {
                            var payday = this.NextPayday;
                            while (payday <= today)
                            {
                                payday = payday.AddDays(7);
                                if (payday.DayOfWeek == DayOfWeek.Saturday)
                                    payday = payday.AddDays(-1);
                                if (payday.DayOfWeek == DayOfWeek.Sunday)
                                    payday = payday.AddDays(-2);
                            }

                            this.NextPayday = payday;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}