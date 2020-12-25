﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using FinanceApp.Api.Enums;
using FinanceApp.API.Enums;

namespace FinanceApp.Api.Entities
{
    public class Bill 
    {
        public Bill()
        {
            DueDate = DateTime.Today;    
        }


        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required, DataType(DataType.Currency)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        [EnumDataType(typeof(FrequencyEnum))]
        [Required, Display(Name = "Frequency")]
        public FrequencyEnum PaymentFrequency { get; set; }

        [EnumDataType(typeof(CategoriesEnum))]
        [Required, Display(Name = "Category")]
        public CategoriesEnum Category { get; set; }

        //public int? AccountId { get; set; }

        //public Account Account { get; set; }
    }

    public class BillDTO : Bill
    {
        public int? FrequencyId { get; set; }
        public int? CategoryId { get; set; }
    }
}