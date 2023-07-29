using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class Updatesomeentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                columns: new[] { "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7494), new DateTime(2023, 5, 24, 14, 9, 17, 453, DateTimeKind.Local).AddTicks(7495) });
        }
    }
}
