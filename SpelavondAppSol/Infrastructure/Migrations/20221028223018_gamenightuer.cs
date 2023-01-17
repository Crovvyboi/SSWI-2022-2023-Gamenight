using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class gamenightuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_Users_playerID",
                table: "userGamenights");

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_Users_playerID",
                table: "userGamenights",
                column: "playerID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userGamenights_Users_playerID",
                table: "userGamenights");

            migrationBuilder.AddForeignKey(
                name: "FK_userGamenights_Users_playerID",
                table: "userGamenights",
                column: "playerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
