using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class modifdbteamname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Teams",
            maxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
