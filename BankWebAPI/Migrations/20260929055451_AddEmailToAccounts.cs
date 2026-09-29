using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email_address",
                table: "bankaccounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_address",
                table: "bankaccounts");
        }
    }
}
