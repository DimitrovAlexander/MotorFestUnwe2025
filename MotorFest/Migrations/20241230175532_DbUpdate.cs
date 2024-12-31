using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorFest.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_OwnerId1",
                schema: "21180022",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerId1",
                schema: "21180022",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                schema: "21180022",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                schema: "21180022",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                schema: "21180022",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_OwnerId",
                schema: "21180022",
                table: "Vehicles",
                column: "OwnerId",
                principalSchema: "21180022",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_OwnerId",
                schema: "21180022",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerId",
                schema: "21180022",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                schema: "21180022",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                schema: "21180022",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId1",
                schema: "21180022",
                table: "Vehicles",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_OwnerId1",
                schema: "21180022",
                table: "Vehicles",
                column: "OwnerId1",
                principalSchema: "21180022",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
