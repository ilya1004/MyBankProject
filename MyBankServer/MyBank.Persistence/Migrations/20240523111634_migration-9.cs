using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditRequestsRejected",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditRequestsRejected",
                table: "Users");
        }
    }
}
