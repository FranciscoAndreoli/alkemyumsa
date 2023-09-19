using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alkemyumsa.Migrations
{
    public partial class prueba1 : Migration
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
                    contrasena_usuario = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    rol = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.cod_usuario);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "cod_usuario", "apellido_usuario", "contrasena_usuario", "deleted_at", "email_usuario", "nombre_usuario", "rol" },
                values: new object[] { 1, "Andreoli", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", null, "franandreoli7@gmail.com", "Francisco", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
