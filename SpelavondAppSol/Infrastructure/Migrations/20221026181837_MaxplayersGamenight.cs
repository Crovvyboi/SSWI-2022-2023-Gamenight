using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MaxplayersGamenight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameNightUser");

            migrationBuilder.AddColumn<int>(
                name: "maxPlayers",
                table: "GameNight",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxPlayers",
                table: "GameNight");

            migrationBuilder.CreateTable(
                name: "GameNightUser",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    playeratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNightUser", x => new { x.PlayersId, x.playeratId });
                    table.ForeignKey(
                        name: "FK_GameNightUser_GameNight_playeratId",
                        column: x => x.playeratId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNightUser_Users_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameNightUser_playeratId",
                table: "GameNightUser",
                column: "playeratId");
        }
    }
}
