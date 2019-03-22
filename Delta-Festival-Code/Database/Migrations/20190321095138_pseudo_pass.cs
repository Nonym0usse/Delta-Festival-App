using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class pseudo_pass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Users");
        }
    }
}
