using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Cards_CardId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditAccounts_CreditAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreditAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_CreditAccounts_CardId",
                table: "CreditAccounts");

            migrationBuilder.DropColumn(
                name: "CreditAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InterestPaymentType",
                table: "DepositAccounts");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CreditAccounts");

            migrationBuilder.DropColumn(
                name: "CreditAccountId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "DateRate",
                table: "Currencies",
                newName: "LastDateRateUpdate");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "CreditRequests",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditGrantedAmount",
                table: "CreditAccounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditGrantedAmount",
                table: "CreditAccounts");

            migrationBuilder.RenameColumn(
                name: "LastDateRateUpdate",
                table: "Currencies",
                newName: "DateRate");

            migrationBuilder.AddColumn<int>(
                name: "CreditAccountId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterestPaymentType",
                table: "DepositAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "CreditRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CreditAccounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditAccountId",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditAccountId",
                table: "Transactions",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_CardId",
                table: "CreditAccounts",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Cards_CardId",
                table: "CreditAccounts",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditAccounts_CreditAccountId",
                table: "Transactions",
                column: "CreditAccountId",
                principalTable: "CreditAccounts",
                principalColumn: "Id");
        }
    }
}
