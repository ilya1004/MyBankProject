using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts"
            );

            migrationBuilder.DropIndex(
                name: "IX_PersonalAccounts_UserId",
                table: "PersonalAccounts"
            );

            migrationBuilder.AddColumn<int>(
                name: "UserOwnerId",
                table: "PersonalAccounts",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_UserOwnerId",
                table: "PersonalAccounts",
                column: "UserOwnerId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Users_UserOwnerId",
                table: "PersonalAccounts",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAccounts_Users_UserOwnerId",
                table: "PersonalAccounts"
            );

            migrationBuilder.DropIndex(
                name: "IX_PersonalAccounts_UserOwnerId",
                table: "PersonalAccounts"
            );

            migrationBuilder.DropColumn(name: "UserOwnerId", table: "PersonalAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_UserId",
                table: "PersonalAccounts",
                column: "UserId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAccounts_Users_UserId",
                table: "PersonalAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }
    }
}
