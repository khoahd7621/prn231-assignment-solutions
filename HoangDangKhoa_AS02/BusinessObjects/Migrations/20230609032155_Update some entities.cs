using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class Updatesomeentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParticipatingProjects",
                keyColumns: new[] { "CompanyProjectID", "EmployeeID" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 6, 10, 10, 21, 55, 365, DateTimeKind.Local).AddTicks(9253), new DateTime(2023, 6, 9, 10, 21, 55, 365, DateTimeKind.Local).AddTicks(9240) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParticipatingProjects",
                keyColumns: new[] { "CompanyProjectID", "EmployeeID" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 6, 7, 19, 2, 28, 717, DateTimeKind.Local).AddTicks(3580), new DateTime(2023, 6, 6, 19, 2, 28, 717, DateTimeKind.Local).AddTicks(3566) });
        }
    }
}
