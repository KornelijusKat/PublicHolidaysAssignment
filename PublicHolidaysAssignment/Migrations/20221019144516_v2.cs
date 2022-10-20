using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHolidaysAssignment.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Date_Holidays_CountryHolidaysCountryId",
                table: "Date");

            migrationBuilder.DropForeignKey(
                name: "FK_Name_Holidays_CountryHolidaysCountryId",
                table: "Name");

            migrationBuilder.DropIndex(
                name: "IX_Name_CountryHolidaysCountryId",
                table: "Name");

            migrationBuilder.DropIndex(
                name: "IX_Date_CountryHolidaysCountryId",
                table: "Date");

            migrationBuilder.DropColumn(
                name: "CountryHolidaysCountryId",
                table: "Name");

            migrationBuilder.DropColumn(
                name: "CountryHolidaysCountryId",
                table: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "DateId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "HolidayType",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Date_DateId",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Name_NameId",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_DateId",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_NameId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "HolidayType",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Holidays");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryHolidaysCountryId",
                table: "Name",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryHolidaysCountryId",
                table: "Date",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Name_CountryHolidaysCountryId",
                table: "Name",
                column: "CountryHolidaysCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Date_CountryHolidaysCountryId",
                table: "Date",
                column: "CountryHolidaysCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Date_Holidays_CountryHolidaysCountryId",
                table: "Date",
                column: "CountryHolidaysCountryId",
                principalTable: "Holidays",
                principalColumn: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Name_Holidays_CountryHolidaysCountryId",
                table: "Name",
                column: "CountryHolidaysCountryId",
                principalTable: "Holidays",
                principalColumn: "CountryId");
        }
    }
}
