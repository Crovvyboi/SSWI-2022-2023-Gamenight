using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddAllergies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVegan",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "nutAlergy",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "toleratesAlcohol",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isAlcoholic",
                table: "Foodstuffs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegan",
                table: "Foodstuffs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "nutAlergy",
                table: "Foodstuffs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVegan",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "nutAlergy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "toleratesAlcohol",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isAlcoholic",
                table: "Foodstuffs");

            migrationBuilder.DropColumn(
                name: "isVegan",
                table: "Foodstuffs");

            migrationBuilder.DropColumn(
                name: "nutAlergy",
                table: "Foodstuffs");
        }
    }
}
