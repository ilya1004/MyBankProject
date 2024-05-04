using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using MyBank.Domain.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_Currencies_CurrencyId",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "InterestPaymentType",
                table: "DepositAccounts");

            migrationBuilder.DropColumn(
                name: "CreditInterestRate",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DepositInterestRate",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreditTermInDays",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "StartBalance",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "TotalPaymentsNumber",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "InterestCalculationType",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "HasPrepaymentOption",
                table: "CreditRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CreditRequests");

            migrationBuilder.AddColumn<int>(
                name: "CreditPackageId",
                table: "CreditRequests",
                type: "integer",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "CreditPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreditStartBalance = table.Column<decimal>(type: "money", nullable: false),
                    CreditGrantedAmount = table.Column<decimal>(type: "money", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    InterestCalculationType = table.Column<string>(type: "text", nullable: false),
                    CreditTermInDays = table.Column<int>(type: "integer", nullable: false),
                    HasPrepaymentOption = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditPackages_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepositPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepositStartBalance = table.Column<decimal>(type: "money", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    DepositTermInDays = table.Column<int>(type: "integer", nullable: false),
                    IsRevocable = table.Column<bool>(type: "boolean", nullable: false),
                    HasCapitalisation = table.Column<bool>(type: "boolean", nullable: false),
                    HasInterestWithdrawalOption = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositPackages_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditPackages_CurrencyId",
                table: "CreditPackages",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPackages_CurrencyId",
                table: "DepositPackages",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_CreditPackages_CreditPackageId",
                table: "CreditRequests",
                column: "CreditPackageId",
                principalTable: "CreditPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditRequests_CreditPackages_CreditPackageId",
                table: "CreditRequests");

            migrationBuilder.DropTable(
                name: "CreditPackages");

            migrationBuilder.DropTable(
                name: "DepositPackages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CreditRequests",
                newName: "InterestCalculationType");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "CreditRequests",
                newName: "HasPrepaymentOption");

            migrationBuilder.RenameColumn(
                name: "CreditPackageId",
                table: "CreditRequests",
                newName: "CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditRequests_CreditPackageId",
                table: "CreditRequests",
                newName: "IX_CreditRequests_CurrencyId");

            migrationBuilder.AddColumn<string>(
                name: "InterestPaymentType",
                table: "DepositAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditInterestRate",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DepositInterestRate",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CreditTermInDays",
                table: "CreditRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestRate",
                table: "CreditRequests",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StartBalance",
                table: "CreditRequests",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalPaymentsNumber",
                table: "CreditRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditRequests_Currencies_CurrencyId",
                table: "CreditRequests",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
