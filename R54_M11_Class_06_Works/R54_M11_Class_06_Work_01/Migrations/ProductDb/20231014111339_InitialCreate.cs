using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace R54_M11_Class_06_Work_01.Migrations.ProductDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "OnSale", "Picture", "Price", "ProductName", "Size" },
                values: new object[,]
                {
                    { 1, false, "1.jpg", 1765m, "Product 1", 2 },
                    { 2, true, "2.jpg", 1040m, "Product 2", 3 },
                    { 3, false, "3.jpg", 1771m, "Product 3", 1 },
                    { 4, true, "4.jpg", 1366m, "Product 4", 2 },
                    { 5, false, "5.jpg", 1886m, "Product 5", 1 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "Date", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 7, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(367), 1, 147 },
                    { 2, new DateTime(2022, 6, 5, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(400), 2, 105 },
                    { 3, new DateTime(2022, 8, 8, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(408), 3, 139 },
                    { 4, new DateTime(2022, 8, 3, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(415), 4, 142 },
                    { 5, new DateTime(2022, 7, 29, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(422), 5, 110 },
                    { 6, new DateTime(2022, 7, 25, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(431), 1, 191 },
                    { 7, new DateTime(2022, 8, 28, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(481), 2, 159 },
                    { 8, new DateTime(2022, 6, 20, 17, 13, 39, 462, DateTimeKind.Local).AddTicks(491), 3, 102 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
