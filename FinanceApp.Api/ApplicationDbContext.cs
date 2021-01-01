﻿using FinanceApp.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Api
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) : base(options)
        {
            _options = options;
            _config = config;
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}