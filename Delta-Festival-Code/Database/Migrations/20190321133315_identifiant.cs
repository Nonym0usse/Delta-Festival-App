using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class identifiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pseudo",
                table: "Users",
                newName: "pseudo");

            migrationBuilder.AddColumn<string>(
                name: "identifiant",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "identifiant",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "pseudo",
                table: "Users",
                newName: "Pseudo");
        }
    }
}
