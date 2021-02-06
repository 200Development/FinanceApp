using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Api.Migrations
{
    public partial class updateAccountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcludeFromSurplus",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsAddNewAccount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsDisposableIncomeAccount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsCashAccount",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCashAccount",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "ExcludeFromSurplus",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAddNewAccount",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisposableIncomeAccount",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
