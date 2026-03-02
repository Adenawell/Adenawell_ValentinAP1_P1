using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adenawell_ValentinAP1_P1.Migrations
{
    /// <inheritdoc />
    public partial class FinalTarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntradasHuacales",
                columns: table => new
                {
                    IdEntrada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasHuacales", x => x.IdEntrada);
                });

            migrationBuilder.CreateTable(
                name: "TiposHuacales",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Existencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposHuacales", x => x.TipoId);
                });

            migrationBuilder.CreateTable(
                name: "DetallesHuacales",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEntrada = table.Column<int>(type: "int", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesHuacales", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_DetallesHuacales_EntradasHuacales_IdEntrada",
                        column: x => x.IdEntrada,
                        principalTable: "EntradasHuacales",
                        principalColumn: "IdEntrada",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesHuacales_TiposHuacales_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposHuacales",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposHuacales",
                columns: new[] { "TipoId", "Descripcion", "Existencia" },
                values: new object[,]
                {
                    { 1, "Rojo", 0 },
                    { 2, "Verde", 0 },
                    { 3, "Amarillo", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesHuacales_IdEntrada",
                table: "DetallesHuacales",
                column: "IdEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesHuacales_TipoId",
                table: "DetallesHuacales",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesHuacales");

            migrationBuilder.DropTable(
                name: "EntradasHuacales");

            migrationBuilder.DropTable(
                name: "TiposHuacales");
        }
    }
}
