using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class Editorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7513));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                columns: new[] { "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7541), new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7542) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 36, 34, 864, DateTimeKind.Local).AddTicks(566));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 36, 34, 864, DateTimeKind.Local).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 25, 16, 36, 34, 864, DateTimeKind.Local).AddTicks(579));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                columns: new[] { "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2023, 5, 25, 16, 36, 34, 864, DateTimeKind.Local).AddTicks(590), new DateTime(2023, 5, 25, 16, 36, 34, 864, DateTimeKind.Local).AddTicks(590) });
        }
    }
}
