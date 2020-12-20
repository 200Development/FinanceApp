using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace FinanceApp.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    PaycheckContribution = table.Column<decimal>(nullable: false),
                    SuggestedPaycheckContribution = table.Column<decimal>(nullable: false),
                    RequiredSavings = table.Column<decimal>(nullable: false),
                    BalanceSurplus = table.Column<decimal>(nullable: false),
                    ExcludeFromSurplus = table.Column<bool>(nullable: false),
                    IsDisposableIncomeAccount = table.Column<bool>(nullable: false),
                    IsEmergencyFund = table.Column<bool>(nullable: false),
                    IsAddNewAccount = table.Column<bool>(nullable: false),
                    IsMandatory = table.Column<bool>(nullable: false),
                    BalanceLimit = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
