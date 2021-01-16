
using System;
using FinanceApp.Api.Enums;

namespace FinanceApp.Api.Models.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public CategoriesEnum Category { get; set; }
        public TransactionTypesEnum Type { get; set; }
    }
}
