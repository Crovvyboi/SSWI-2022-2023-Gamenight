using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class gamenightuer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights");

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights",
                column: "gameNightID",
                principalTable: "GameNight",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights");

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights",
                column: "gameNightID",
                principalTable: "GameNight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
