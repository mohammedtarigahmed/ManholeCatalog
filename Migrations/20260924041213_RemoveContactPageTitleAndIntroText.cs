using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManholeCatalog.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContactPageTitleAndIntroText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntroText",
                table: "ContactPages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContactPages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntroText",
                table: "ContactPages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContactPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
