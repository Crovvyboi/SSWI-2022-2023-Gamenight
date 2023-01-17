using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class F : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GameNightUser_Users_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameNightUser_playeratId",
                table: "GameNightUser",
                column: "playeratId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameNightUser");

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
    }
}
