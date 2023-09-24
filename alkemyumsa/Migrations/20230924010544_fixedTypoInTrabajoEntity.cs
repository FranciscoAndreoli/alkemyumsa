using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alkemyumsa.Migrations
{
    public partial class fixedTypoInTrabajoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cod_trabjo",
                table: "Trabajo",
                newName: "cod_trabajo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cod_trabajo",
                table: "Trabajo",
                newName: "cod_trabjo");
        }
    }
}
