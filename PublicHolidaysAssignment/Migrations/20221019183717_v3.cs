using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHolidaysAssignment.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Date_DateId",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Name_NameId",
                table: "Holidays");

            migrationBuilder.DropTable(
                name: "Date");

            migrationBuilder.DropTable(
                name: "Name");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_DateId",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_NameId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Holidays");

            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "Holidays",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Lang",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LangEn",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextEn",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Lang",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "LangEn",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "TextEn",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Holidays");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Holidays",
                newName: "NameId");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DateId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays",
                column: "CountryId");

            migrationBuilder.CreateTable(
                name: "Date",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfTheWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Name",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Name", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_DateId",
                table: "Holidays",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_NameId",
                table: "Holidays",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Date_DateId",
                table: "Holidays",
                column: "DateId",
                principalTable: "Date",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Name_NameId",
                table: "Holidays",
                column: "NameId",
                principalTable: "Name",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
