using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APPR.coreproject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationTransactionReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "Donations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "Donations");
        }
    }
}
