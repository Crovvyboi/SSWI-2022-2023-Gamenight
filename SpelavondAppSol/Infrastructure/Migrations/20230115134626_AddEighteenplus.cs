using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddEighteenplus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights");

            migrationBuilder.AddColumn<bool>(
                name: "isEighteenPlus",
                table: "GameNight",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights",
                column: "gameNightID",
                principalTable: "GameNight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights");

            migrationBuilder.DropColumn(
                name: "isEighteenPlus",
                table: "GameNight");

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_GameNight_gameNightID",
                table: "userGamenights",
                column: "gameNightID",
                principalTable: "GameNight",
                principalColumn: "Id");
        }
    }
}
