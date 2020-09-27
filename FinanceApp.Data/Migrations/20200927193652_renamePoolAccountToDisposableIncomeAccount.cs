using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Data.Migrations
{
    public partial class renamePoolAccountToDisposableIncomeAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPoolAccount",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisposableIncomeAccount",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisposableIncomeAccount",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPoolAccount",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
