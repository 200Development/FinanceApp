using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Api.Migrations
{
    public partial class modifyExpensesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Bills_BillId1",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BillId1",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BillId1",
                table: "Expenses");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Expenses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsBill",
                table: "Expenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Expenses",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PayDeduction",
                table: "Expenses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFrequency",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_AccountId",
                table: "Expenses",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_AccountId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsBill",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PayDeduction",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillId1",
                table: "Expenses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BillId1",
                table: "Expenses",
                column: "BillId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Bills_BillId1",
                table: "Expenses",
                column: "BillId1",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
