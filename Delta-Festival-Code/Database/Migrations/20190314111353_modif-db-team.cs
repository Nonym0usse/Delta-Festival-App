using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Database.Migrations
{
    public partial class modifdbteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WinDate",
                table: "Teams",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
