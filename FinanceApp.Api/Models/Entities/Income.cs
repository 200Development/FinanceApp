using System;
using System.ComponentModel.DataAnnotations;
using FinanceApp.API.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Income
    {
        public Income()
        {
            UpdateNextPayday();
        }

        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        public string Payee { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextPayday { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [EnumDataType(typeof(FrequencyEnum))]
        [Required, Display(Name = "Pay Frequency")]
        public FrequencyEnum PaymentFrequency { get; set; }

        public int? FirstMonthlyPayDay { get; set; }

        public int? SecondMonthlyPayDay { get; set; }

        private void UpdateNextPayday()
        {
            var today = DateTime.Now;

            if (this.NextPayday >= today)
            {
                switch (PaymentFrequency)
                {
                    case FrequencyEnum.Annually:
                        this.NextPayday = this.NextPayday.AddYears(1);
                        break;
                    case FrequencyEnum.BiAnnually:
                        this.NextPayday = this.NextPayday.AddMonths(6);
                        break;
                    case FrequencyEnum.Quarterly:
                        this.NextPayday = this.NextPayday.AddMonths(3);
                        break;
                    case FrequencyEnum.Monthly:
                        this.NextPayday = this.NextPayday.AddMonths(1);
                        break;
                    case FrequencyEnum.BiMonthly:
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
                    case FrequencyEnum.BiWeekly:
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
                    case FrequencyEnum.Weekly:
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
                    case FrequencyEnum.Daily:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public decimal GetMonthlyIncome()
        {
            return this.PaymentFrequency switch
            {
                FrequencyEnum.Annually => this.Amount / 12,
                FrequencyEnum.BiAnnually => this.Amount / 6,
                FrequencyEnum.Quarterly => this.Amount / 3,
                FrequencyEnum.Monthly => this.Amount,
                FrequencyEnum.BiMonthly => this.Amount * 2,
                FrequencyEnum.BiWeekly => this.Amount * 2,// TODO: Refactor to account for the 2 extra paychecks per year
                FrequencyEnum.Weekly => this.Amount * 52 / 12,
                FrequencyEnum.Daily => this.Amount * 364.25m / 12,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public decimal? GetOutstandingMonthlyIncome()
        {
            var today = DateTime.Today;
            var nextPayday = this.NextPayday;

            switch (this.PaymentFrequency)
            {
                case FrequencyEnum.Annually:
                    throw new ArgumentOutOfRangeException();
                case FrequencyEnum.BiAnnually:
                    throw new ArgumentOutOfRangeException();
                case FrequencyEnum.Quarterly:
                    throw new ArgumentOutOfRangeException();
                case FrequencyEnum.Monthly:
                    throw new ArgumentOutOfRangeException();
                case FrequencyEnum.BiMonthly:
                    var secondMonthlyPayDay = this.SecondMonthlyPayDay;
                    if (secondMonthlyPayDay != null && today < new DateTime(today.Year, today.Month, secondMonthlyPayDay.Value))
                    {
                        var firstMonthlyPayDay = this.FirstMonthlyPayDay;
                        if (firstMonthlyPayDay != null && today < new DateTime(today.Year, today.Month, firstMonthlyPayDay.Value))
                            return this.Amount * 2;

                        return this.Amount;
                    }

                    return 0.0m;
                case FrequencyEnum.BiWeekly:

                    if (today.Month == nextPayday.Month)
                        if (today.Month == nextPayday.AddDays(14).Month)
                        {
                            if (today.Month == nextPayday.AddDays(28).Month)
                                return Amount * 3;

                            return Amount * 2;
                        }
                        else
                        {
                            return Amount;
                        }

                    return 0.0m;
                case FrequencyEnum.Weekly:
                    throw new ArgumentOutOfRangeException();
                case FrequencyEnum.Daily:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}