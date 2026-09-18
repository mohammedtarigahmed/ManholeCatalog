using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManholeCatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddQuotationPublicToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicToken",
                table: "QuotationRequests",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicToken",
                table: "QuotationRequests");
        }
    }
}
