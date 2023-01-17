using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class E : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameNightId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GameNightId",
                table: "Users",
                column: "GameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GameNight_GameNightId",
                table: "Users",
                column: "GameNightId",
                principalTable: "GameNight",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GameNight_GameNightId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GameNightId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameNightId",
                table: "Users");
        }
    }
}
