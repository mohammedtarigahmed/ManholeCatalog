using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManholeCatalog.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategorySeedAndSetPricePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Covers for manholes and inspection chambers.", true, "Manhole Covers" },
                    { 2, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Covers and grates for drainage applications.", true, "Drainage Covers" },
                    { 3, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frames and related installation components.", true, "Frames" },
                    { 4, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accessories and additional components.", true, "Accessories" }
                });
        }
    }
}
