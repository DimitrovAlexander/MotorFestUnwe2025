using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorFest.Migrations
{
    /// <inheritdoc />
    public partial class LocationIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "21180022",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "21180022",
                table: "Locations");
        }
    }
}
