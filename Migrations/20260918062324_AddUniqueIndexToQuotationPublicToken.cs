using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManholeCatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToQuotationPublicToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_PublicToken",
                table: "QuotationRequests",
                column: "PublicToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuotationRequests_PublicToken",
                table: "QuotationRequests");
        }
    }
}
