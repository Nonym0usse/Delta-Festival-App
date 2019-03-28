using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class user_like_score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeScore",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeScore",
                table: "Users");
        }
    }
}
