using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AdmonEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoIdentificacion = table.Column<int>(type: "int", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalarioBaseMensual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "TiposIdentificacion");
        }
    }
}
