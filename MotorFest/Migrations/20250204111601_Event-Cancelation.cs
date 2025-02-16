using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorFest.Migrations
{
    /// <inheritdoc />
    public partial class EventCancelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                schema: "21180022",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                schema: "21180022",
                table: "Events");
        }
    }
}
