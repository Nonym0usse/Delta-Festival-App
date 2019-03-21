using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class active_default_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Teams",
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Teams",
                oldDefaultValue: false);
        }
    }
}
