using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alkemyumsa.Migrations
{
    public partial class addedEntitiesAndSeeders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    cod_proyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_proyecto = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    direccion = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    estado = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.cod_proyecto);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    cod_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descr_servicio = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    estado_servicio = table.Column<bool>(type: "bit", nullable: false),
                    valor_hora = table.Column<float>(type: "real", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.cod_servicio);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    cod_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    dni = table.Column<int>(type: "INT", nullable: false),
                    email_usuario = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    contrasena_usuario = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    rol = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.cod_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Trabajo",
                columns: table => new
                {
                    cod_trabjo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cant_horas = table.Column<int>(type: "int", nullable: false),
                    valor_hora = table.Column<double>(type: "float", nullable: false),
                    costo = table.Column<double>(type: "float", nullable: false),
                    cod_proyecto = table.Column<int>(type: "int", nullable: false),
                    cod_servicio = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajo", x => x.cod_trabjo);
                    table.ForeignKey(
                        name: "FK_Trabajo_Proyecto_cod_proyecto",
                        column: x => x.cod_proyecto,
                        principalTable: "Proyecto",
                        principalColumn: "cod_proyecto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajo_Servicio_cod_servicio",
                        column: x => x.cod_servicio,
                        principalTable: "Servicio",
                        principalColumn: "cod_servicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Proyecto",
                columns: new[] { "cod_proyecto", "deleted_at", "direccion", "estado", "nombre_proyecto" },
                values: new object[,]
                {
                    { 1, null, "Sector Montañoso El Olimpo, Bogotá.", "Pendiente", "Sistema de información TechOil" },
                    { 2, null, "Base Naval Atlántica, Buenos Aires.", "Confirmado", "Plataforma petrolera" },
                    { 3, null, "Parque Científico Gaia, Santiago.", "Confirmado", "Perforación petrolera" },
                    { 4, null, "Torre Empresarial Orion, Piso 20, Lima.", "Terminado", "Capacitación de personal" }
                });

            migrationBuilder.InsertData(
                table: "Servicio",
                columns: new[] { "cod_servicio", "deleted_at", "descr_servicio", "estado_servicio", "valor_hora" },
                values: new object[,]
                {
                    { 1, null, "Desarrollo y mantenimiento de sistemas de información para gestión petrolera.", true, 190000f },
                    { 2, null, "Instalación de plataformas petroleras en alta mar.", true, 1450000f },
                    { 3, null, "Perforación exploratoria en terrenos offshore.", true, 2200000f },
                    { 4, null, "Capacitaciones y entrenamientos en seguridad industrial petrolera.", false, 170000f }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "cod_usuario", "contrasena_usuario", "deleted_at", "dni", "email_usuario", "nombre_usuario", "rol" },
                values: new object[,]
                {
                    { 1, "0be83396efc24a489442a7a903cb659a67ce5d47c5bbbf571f8bcf418e6f5e39", null, 42641623, "franandreoli7@gmail.com", "Francisco", 1 },
                    { 2, "6581e8e1b74cae82b504659248ac9c5cb107530078e9426e6cea85aed2c327cf", null, 38543765, "josé@gmail.com", "José", 2 }
                });

            migrationBuilder.InsertData(
                table: "Trabajo",
                columns: new[] { "cod_trabjo", "cant_horas", "cod_proyecto", "cod_servicio", "costo", "deleted_at", "fecha", "valor_hora" },
                values: new object[,]
                {
                    { 1, 110, 1, 1, 20900000.0, null, new DateTime(2022, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 190000.0 },
                    { 2, 200, 2, 2, 290000000.0, null, new DateTime(2022, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1450000.0 },
                    { 3, 300, 3, 3, 660000000.0, null, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200000.0 },
                    { 4, 24, 4, 4, 4080000.0, null, new DateTime(2022, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 170000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_cod_proyecto",
                table: "Trabajo",
                column: "cod_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_cod_servicio",
                table: "Trabajo",
                column: "cod_servicio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trabajo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "Servicio");
        }
    }
}
