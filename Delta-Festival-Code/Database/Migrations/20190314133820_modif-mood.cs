using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class modifmood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "Mood",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Libelle",
                table: "Mood");
        }
    }
}
