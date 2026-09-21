using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBookWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeededProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Author 1", 1, "Product 1", "ISBN 1", null, 50.0, 45.0, 35.0, 40.0, "Product 1" },
                    { 2, "Author 2", 2, "Product 2", "ISBN 2", null, 50.0, 45.0, 35.0, 40.0, "Product 2" },
                    { 3, "Author 3", 3, "Product 3", "ISBN 3", null, 50.0, 45.0, 35.0, 40.0, "Product 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
