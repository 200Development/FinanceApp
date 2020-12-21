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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Uid=jpFinanceUser;Pwd=kUuT_2hM.oHU7Rkti*hPuUhs9kyzje;Database=jpfinancial;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
                entity.Property(e => e.BalanceLimit).HasColumnType("decimal(18,2)");
                entity.Property(e => e.BalanceSurplus).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaycheckContribution).HasColumnType("decimal(18,2)");
                entity.Property(e => e.RequiredSavings).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SuggestedPaycheckContribution).HasColumnType("decimal(18,2)");
            });
        }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
