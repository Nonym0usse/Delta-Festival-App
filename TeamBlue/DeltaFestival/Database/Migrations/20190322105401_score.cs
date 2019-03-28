using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
