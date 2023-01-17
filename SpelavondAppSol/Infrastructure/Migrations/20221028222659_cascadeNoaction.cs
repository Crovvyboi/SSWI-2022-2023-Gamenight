using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class cascadeNoaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foodstuffs_GameNight_GameNightId",
                table: "Foodstuffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Foodstuffs_Users_userid",
                table: "Foodstuffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Game_GameID",
                table: "GameNight");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Users_OrganizerID",
                table: "GameNight");

            migrationBuilder.AddForeignKey(
                name: "FK_Foodstuffs_GameNight_GameNightId",
                table: "Foodstuffs",
                column: "GameNightId",
                principalTable: "GameNight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foodstuffs_Users_userid",
                table: "Foodstuffs",
                column: "userid",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Game_GameID",
                table: "GameNight",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Users_OrganizerID",
                table: "GameNight",
                column: "OrganizerID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foodstuffs_GameNight_GameNightId",
                table: "Foodstuffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Foodstuffs_Users_userid",
                table: "Foodstuffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Game_GameID",
                table: "GameNight");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNight_Users_OrganizerID",
                table: "GameNight");

            migrationBuilder.AddForeignKey(
                name: "FK_Foodstuffs_GameNight_GameNightId",
                table: "Foodstuffs",
                column: "GameNightId",
                principalTable: "GameNight",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foodstuffs_Users_userid",
                table: "Foodstuffs",
                column: "userid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Game_GameID",
                table: "GameNight",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNight_Users_OrganizerID",
                table: "GameNight",
                column: "OrganizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
