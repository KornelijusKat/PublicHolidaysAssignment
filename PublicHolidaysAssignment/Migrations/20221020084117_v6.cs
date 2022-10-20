using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHolidaysAssignment.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "DayStatuses");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "DayStatuses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "DayStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "DayStatuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DayStatuses");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "DayStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "DayStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "DayStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
