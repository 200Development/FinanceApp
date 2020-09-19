using FinanceApp.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        
        public DbSet<Account> Accounts { get; set; } 
        public DbSet<Bill> Bills { get; set; } 
        public DbSet<Expense> Expenses { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>()
                .Property(p => p.Balance)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Account>()
                .Property(p => p.BalanceLimit)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Account>()
                .Property(p => p.BalanceSurplus)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Account>()
                .Property(p => p.PaycheckContribution)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Account>()
                .Property(p => p.RequiredSavings)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Account>()
                .Property(p => p.SuggestedPaycheckContribution)
                .HasColumnType("decimal(18,2)");
            

            builder.Entity<Bill>()
                .Property(p => p.AmountDue)
                .HasColumnType("decimal(18,2)");
            
            builder.Entity<Expense>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");


        }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
