using FinanceApp.Api.Entities;
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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=localhost;Port=3306;Uid=jpFinanceUser;Pwd=kUuT_2hM.oHU7Rkti*hPuUhs9kyzje;Database=jpfinancial;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Account>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
        //        entity.Property(e => e.BalanceLimit).HasColumnType("decimal(18,2)");
        //        entity.Property(e => e.BalanceSurplus).HasColumnType("decimal(18,2)");
        //        entity.Property(e => e.PaycheckContribution).HasColumnType("decimal(18,2)");
        //        entity.Property(e => e.RequiredSavings).HasColumnType("decimal(18,2)");
        //        entity.Property(e => e.SuggestedPaycheckContribution).HasColumnType("decimal(18,2)");
        //    });
        //}
    }
}
