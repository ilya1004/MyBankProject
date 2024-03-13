using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardPackages_CardPackageId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Credits_CreditAccountId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditPayments_Credits_CreditAccountId",
                table: "CreditPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditPayments_Users_UserId",
                table: "CreditPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_Moderators_ModeratorId",
                table: "CreditRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_Users_UserId",
                table: "CreditRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Credits_Currencies_CurrencyId",
                table: "Credits");

            migrationBuilder.DropForeignKey(
                name: "FK_Credits_Moderators_ModeratorApprovedId",
                table: "Credits");

            migrationBuilder.DropForeignKey(
                name: "FK_Credits_Users_UserId",
                table: "Credits");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositAccruals_Deposits_DepositAccountId",
                table: "DepositAccruals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Currencies_CurrencyId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Users_UserId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Currencies_CurrencyId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Credits_CreditAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CreditAccountId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credits",
                table: "Credits");

            migrationBuilder.RenameTable(
                name: "Deposits",
                newName: "DepositAccounts");

            migrationBuilder.RenameTable(
                name: "Credits",
                newName: "CreditAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_Deposits_UserId",
                table: "DepositAccounts",
                newName: "IX_DepositAccounts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deposits_CurrencyId",
                table: "DepositAccounts",
                newName: "IX_DepositAccounts_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Credits_UserId",
                table: "CreditAccounts",
                newName: "IX_CreditAccounts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Credits_ModeratorApprovedId",
                table: "CreditAccounts",
                newName: "IX_CreditAccounts_ModeratorApprovedId");

            migrationBuilder.RenameIndex(
                name: "IX_Credits_CurrencyId",
                table: "CreditAccounts",
                newName: "IX_CreditAccounts_CurrencyId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PersonalAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PersonalAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DepositAccountId",
                table: "DepositAccruals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CreditRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorId",
                table: "CreditRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CreditPayments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreditAccountId",
                table: "CreditPayments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CardPackageId",
                table: "Cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DepositAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "DepositAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CreditAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorApprovedId",
                table: "CreditAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "CreditAccounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CreditAccounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepositAccounts",
                table: "DepositAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditAccounts",
                table: "CreditAccounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_CardId",
                table: "CreditAccounts",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardPackages_CardPackageId",
                table: "Cards",
                column: "CardPackageId",
                principalTable: "CardPackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Cards_CardId",
                table: "CreditAccounts",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Currencies_CurrencyId",
                table: "CreditAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Moderators_ModeratorApprovedId",
                table: "CreditAccounts",
                column: "ModeratorApprovedId",
                principalTable: "Moderators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditPayments_CreditAccounts_CreditAccountId",
                table: "CreditPayments",
                column: "CreditAccountId",
                principalTable: "CreditAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditPayments_Users_UserId",
                table: "CreditPayments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_Moderators_ModeratorId",
                table: "CreditRequests",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_Users_UserId",
                table: "CreditRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositAccounts_Currencies_CurrencyId",
                table: "DepositAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositAccounts_Users_UserId",
                table: "DepositAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositAccruals_DepositAccounts_DepositAccountId",
                table: "DepositAccruals",
                column: "DepositAccountId",
                principalTable: "DepositAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Currencies_CurrencyId",
                table: "PersonalAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditAccounts_CreditAccountId",
                table: "Transactions",
                column: "CreditAccountId",
                principalTable: "CreditAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardPackages_CardPackageId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Cards_CardId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Currencies_CurrencyId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Moderators_ModeratorApprovedId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditPayments_CreditAccounts_CreditAccountId",
                table: "CreditPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditPayments_Users_UserId",
                table: "CreditPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_Moderators_ModeratorId",
                table: "CreditRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_Users_UserId",
                table: "CreditRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositAccounts_Currencies_CurrencyId",
                table: "DepositAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositAccounts_Users_UserId",
                table: "DepositAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositAccruals_DepositAccounts_DepositAccountId",
                table: "DepositAccruals");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Currencies_CurrencyId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditAccounts_CreditAccountId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepositAccounts",
                table: "DepositAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditAccounts",
                table: "CreditAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CreditAccounts_CardId",
                table: "CreditAccounts");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CreditAccounts");

            migrationBuilder.RenameTable(
                name: "DepositAccounts",
                newName: "Deposits");

            migrationBuilder.RenameTable(
                name: "CreditAccounts",
                newName: "Credits");

            migrationBuilder.RenameIndex(
                name: "IX_DepositAccounts_UserId",
                table: "Deposits",
                newName: "IX_Deposits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DepositAccounts_CurrencyId",
                table: "Deposits",
                newName: "IX_Deposits_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditAccounts_UserId",
                table: "Credits",
                newName: "IX_Credits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditAccounts_ModeratorApprovedId",
                table: "Credits",
                newName: "IX_Credits_ModeratorApprovedId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditAccounts_CurrencyId",
                table: "Credits",
                newName: "IX_Credits_CurrencyId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PersonalAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PersonalAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepositAccountId",
                table: "DepositAccruals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CreditRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorId",
                table: "CreditRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CreditPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditAccountId",
                table: "CreditPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardPackageId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Deposits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Deposits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModeratorApprovedId",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credits",
                table: "Credits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CreditAccountId",
                table: "Cards",
                column: "CreditAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardPackages_CardPackageId",
                table: "Cards",
                column: "CardPackageId",
                principalTable: "CardPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Credits_CreditAccountId",
                table: "Cards",
                column: "CreditAccountId",
                principalTable: "Credits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditPayments_Credits_CreditAccountId",
                table: "CreditPayments",
                column: "CreditAccountId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditPayments_Users_UserId",
                table: "CreditPayments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_Moderators_ModeratorId",
                table: "CreditRequests",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_Users_UserId",
                table: "CreditRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Currencies_CurrencyId",
                table: "Credits",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Moderators_ModeratorApprovedId",
                table: "Credits",
                column: "ModeratorApprovedId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Users_UserId",
                table: "Credits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositAccruals_Deposits_DepositAccountId",
                table: "DepositAccruals",
                column: "DepositAccountId",
                principalTable: "Deposits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Currencies_CurrencyId",
                table: "Deposits",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Users_UserId",
                table: "Deposits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Currencies_CurrencyId",
                table: "PersonalAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Credits_CreditAccountId",
                table: "Transactions",
                column: "CreditAccountId",
                principalTable: "Credits",
                principalColumn: "Id");
        }
    }
}
