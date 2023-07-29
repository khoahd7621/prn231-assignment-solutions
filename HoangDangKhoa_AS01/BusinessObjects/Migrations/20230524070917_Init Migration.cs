using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    City = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Freight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowerBouquets",
                columns: table => new
                {
                    FlowerBouquetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerBouquetName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    FlowerBouquetStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerBouquets", x => x.FlowerBouquetID);
                    table.ForeignKey(
                        name: "FK_FlowerBouquets_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerBouquets_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FlowerBouquetID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.FlowerBouquetID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_FlowerBouquets_FlowerBouquetID",
                        column: x => x.FlowerBouquetID,
                        principalTable: "FlowerBouquets",
                        principalColumn: "FlowerBouquetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Rose", "This is Rose" },
                    { 2, "Peonies", "This is Peonies" },
                    { 3, "Lily", "This is Lily" },
                    { 4, "Carnation", "This is Carnation" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "City", "Country", "CustomerName", "DateOfBirth", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "Ho Chi Minh", "VietNam", "Hoang Dang Khoa", new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7472), "khoa.hoangdang@gmail.com", "123456", "Customer" },
                    { 2, "Vung Tau", "VietNam", "Ho Hai Nam", new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7482), "nam.hohai@gmail.com", "123456", "Customer" },
                    { 3, "Binh Duong", "VietNam", "Thai Thanh Phat", new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7483), "phat.thaithanh@gmail.com", "123456", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierID", "SupplierAddress", "SupplierName", "Telephone" },
                values: new object[,]
                {
                    { 1, "46 Nguyen Thai Binh, Ho Chi Minh", "Flower Shop Ciaoflora", "0123456789" },
                    { 2, "46 Nguyen Chanh Sat, Vung Tau", "Ant Flower", "0123456789" }
                });

            migrationBuilder.InsertData(
                table: "FlowerBouquets",
                columns: new[] { "FlowerBouquetID", "CategoryID", "Description", "FlowerBouquetName", "FlowerBouquetStatus", "SupplierID", "UnitPrice", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 1, "This is Rose", "Red Rose", 1, 1, 100000, 100 },
                    { 2, 1, "This is Orchis", "Orchis", 1, 1, 100000, 100 },
                    { 3, 1, "This is Sun Flower", "Sun Flower", 1, 1, 100000, 100 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerID", "Freight", "OrderDate", "OrderStatus", "ShippedDate", "Total" },
                values: new object[] { 1, 1, "COD", new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7494), 1, new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7495), 300000 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "FlowerBouquetID", "OrderID", "Discount", "Quantity", "UnitPrice" },
                values: new object[] { 1, 1, 0, 1, 100000 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "FlowerBouquetID", "OrderID", "Discount", "Quantity", "UnitPrice" },
                values: new object[] { 2, 1, 0, 2, 100000 });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerBouquets_CategoryID",
                table: "FlowerBouquets",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerBouquets_SupplierID",
                table: "FlowerBouquets",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FlowerBouquetID",
                table: "OrderDetails",
                column: "FlowerBouquetID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "FlowerBouquets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
