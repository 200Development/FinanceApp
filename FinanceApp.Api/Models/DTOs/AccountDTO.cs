﻿using System.Collections.Generic;
using FinanceApp.Api.Models.Entities;

namespace FinanceApp.Api.Models.DTOs
{
    public class DTO
    {
        public IList<AccountDto> AccountDtos { get; set; }
        public decimal? SumOfAccountBalances { get; set; }
        public decimal? TotalSurplus { get; set; }
    }

    public class AccountDto
    {
        public Account Account { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public decimal BillSum { get; set; }
        public decimal? PayDeduction { get; set; }
        public decimal? PaycheckPercentage { get; set; }
        public decimal? ExpensesBeforeNextPaycheck { get; set; }
        public decimal? BalanceSurplus { get; set; }
        public decimal? RequiredSavings { get; set; }
    }
}
