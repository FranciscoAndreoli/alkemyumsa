using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alkemyumsa.Migrations
{
    public partial class techOil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    cod_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    apellido_usuario = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    email_usuario = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    contrasena_usuario = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.cod_usuario);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "cod_usuario", "apellido_usuario", "contrasena_usuario", "email_usuario", "nombre_usuario" },
                values: new object[] { 1, "Andreoli", "123456", "franandreoli7@gmail.com", "Francisco" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
