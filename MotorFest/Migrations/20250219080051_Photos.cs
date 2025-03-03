using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorFest.Migrations
{
    /// <inheritdoc />
    public partial class Photos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventVehicleCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "21180022",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventLogo",
                schema: "21180022",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventPhotos",
                schema: "21180022",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<DateTime>(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventEngineTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventVehicleCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "21180022",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventLogo",
                schema: "21180022",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventPhotos",
                schema: "21180022",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "21180022_LastUpdate",
                schema: "21180022",
                table: "EventEngineTypes");
        }
    }
}
