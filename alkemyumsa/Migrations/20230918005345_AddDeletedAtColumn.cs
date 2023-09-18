using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alkemyumsa.Migrations
{
    public partial class AddDeletedAtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "Usuario",
                type: "DATETIME",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "Usuario");
        }
    }
}
