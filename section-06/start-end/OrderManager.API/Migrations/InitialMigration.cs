using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManager.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVendor",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    VendorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVendor", x => new { x.ProductsId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_ProductVendor_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVendor_Vendors_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Description", "OrderDate", "OrderTotal", "ShippingDate", "Title" },
                values: new object[,]
                {
                    { 1, "Office gadgets and laptops", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200.00m, null, "Office Supplies" },
                    { 2, "Personal use gadgets", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 800.00m, null, "Personal Tech" },
                    { 3, "Assorted tech gadgets", new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1240.00m, null, "Tech Gear" },
                    { 4, "Gaming and audio", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1020.00m, null, "Entertainment Bundle" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "High performance laptop", "Laptop", 1200.00m },
                    { 2, "Latest model smartphone", "Smartphone", 800.00m },
                    { 3, "Portable touchscreen tablet", "Tablet", 300.00m },
                    { 4, "Wearable smart device", "Smartwatch", 200.00m },
                    { 5, "Electronic book reader", "E-Reader", 150.00m },
                    { 6, "Bluetooth earphones", "Wireless Earbuds", 90.00m },
                    { 7, "Virtual reality headset", "VR Headset", 350.00m },
                    { 8, "Compact, durable camera", "Action Camera", 400.00m },
                    { 9, "Wearable activity tracker", "Fitness Tracker", 100.00m },
                    { 10, "Wireless, durable speaker", "Portable Speaker", 120.00m },
                    { 11, "Home video game console", "Gaming Console", 500.00m },
                    { 12, "Remote-controlled drone", "Drone", 600.00m }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "Id", "Address", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "123 Tech Lane", "contact@techsupplies.com", "Tech Supplies Inc." },
                    { 2, "456 Gadget St.", "info@gadgetsworld.com", "Gadgets World" },
                    { 3, "789 Tech Park", "sales@globaltech.com", "Global Tech" },
                    { 4, "1011 Innovation Drive", "contact@innovatechsolutions.com", "Innovatech Solutions" },
                    { 5, "1213 Gadget Ave", "support@gadgetplanet.com", "Gadget Planet" },
                    { 6, "1415 Trendy St", "info@techtrends.com", "Tech Trends" },
                    { 7, "1617 Electronics Blvd", "help@electronix.com", "Electronix" }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "Id", "Amount", "Details", "OrderId", "Price", "ProductId" },
                values: new object[,]
                {
                    { 1, 2, "Laptop for office use", 1, 1200.00m, 1 },
                    { 2, 1, "Smartphone for personal use", 2, 800.00m, 2 },
                    { 3, 2, "Tablet for on-the-go entertainment", 3, 300.00m, 3 },
                    { 4, 1, "Smartwatch to stay connected", 3, 200.00m, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ProductId",
                table: "OrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVendor_VendorsId",
                table: "ProductVendor",
                column: "VendorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "ProductVendor");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
