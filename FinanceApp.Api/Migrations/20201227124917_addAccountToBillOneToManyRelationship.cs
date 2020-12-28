using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Api.Migrations
{
    public partial class addAccountToBillOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PayDeduction",
                table: "Bills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountId",
                table: "Bills",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_AccountId",
                table: "Bills",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_AccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PayDeduction",
                table: "Bills");
        }
    }
}
