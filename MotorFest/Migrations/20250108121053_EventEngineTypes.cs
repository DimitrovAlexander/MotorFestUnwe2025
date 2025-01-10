using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorFest.Migrations
{
    /// <inheritdoc />
    public partial class EventEngineTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxYearOfManufacture",
                schema: "21180022",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinYearOfManufacture",
                schema: "21180022",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventEngineTypes",
                schema: "21180022",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EngineTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEngineTypes", x => new { x.EventId, x.EngineTypeId });
                    table.ForeignKey(
                        name: "FK_EventEngineTypes_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalSchema: "21180022",
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventEngineTypes_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "21180022",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEngineTypes_EngineTypeId",
                schema: "21180022",
                table: "EventEngineTypes",
                column: "EngineTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEngineTypes",
                schema: "21180022");

            migrationBuilder.DropColumn(
                name: "MaxYearOfManufacture",
                schema: "21180022",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinYearOfManufacture",
                schema: "21180022",
                table: "Events");
        }
    }
}
