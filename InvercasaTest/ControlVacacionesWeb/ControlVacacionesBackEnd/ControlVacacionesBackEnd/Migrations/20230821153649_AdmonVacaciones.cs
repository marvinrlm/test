using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AdmonVacaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[EstadoVacacion](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [EstadosVacacion] [nvarchar](50) NOT NULL,
                 CONSTRAINT [PK_EstadosVacaciones] PRIMARY KEY CLUSTERED 
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO
                ");


            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[RegistroVacacion](
	                [IdEmpleado] [int] NOT NULL,
	                [FechaVacacion] [date] NOT NULL,
	                [IdEstadoVacacion] [int] NOT NULL,
                 CONSTRAINT [PK_RegistroVacacion] PRIMARY KEY CLUSTERED 
                (
	                [IdEmpleado] ASC,
	                [FechaVacacion] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO

                ALTER TABLE [dbo].[RegistroVacacion]  WITH CHECK ADD  CONSTRAINT [FK_RegistroVacacion_EstadoVacacion] FOREIGN KEY([IdEstadoVacacion])
                REFERENCES [dbo].[EstadoVacacion] ([Id])
                GO

                ALTER TABLE [dbo].[RegistroVacacion] CHECK CONSTRAINT [FK_RegistroVacacion_EstadoVacacion]
                GO
                ");

            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[VacacionSaldo](
	                [IdEmpleado] [int] NOT NULL,
	                [Ano] [int] NOT NULL,
	                [Mes] [int] NOT NULL,
	                [VacacionesAcumuladas] [decimal](18, 2) NOT NULL,
	                [VacacionesTomadas] [decimal](18, 2) NOT NULL,
                 CONSTRAINT [PK_VacacionSaldo] PRIMARY KEY CLUSTERED 
                (
	                [IdEmpleado] ASC,
	                [Ano] ASC,
	                [Mes] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO

                ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[VistaRegistroVacaciones]
                AS
                SELECT dbo.RegistroVacacion.IdEmpleado, dbo.Empleado.NombreCompleto, dbo.RegistroVacacion.FechaVacacion, dbo.EstadoVacacion.EstadosVacacion
                FROM     dbo.Empleado LEFT JOIN
                                  dbo.RegistroVacacion ON dbo.Empleado.Id = dbo.RegistroVacacion.IdEmpleado LEFT JOIN
                                  dbo.EstadoVacacion ON dbo.RegistroVacacion.IdEstadoVacacion = dbo.EstadoVacacion.Id
                GO
                ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[VistaVacacionesConsolidadas]
                AS
                SELECT dbo.VacacionSaldo.IdEmpleado, dbo.Empleado.NombreCompleto, SUM(dbo.VacacionSaldo.VacacionesAcumuladas) AS Acumulado, SUM(dbo.VacacionSaldo.VacacionesTomadas) AS Tomadas, 
                                  SUM(dbo.VacacionSaldo.VacacionesAcumuladas) - SUM(dbo.VacacionSaldo.VacacionesTomadas) AS Saldo
                FROM     dbo.VacacionSaldo LEFT JOIN
                                  dbo.Empleado ON dbo.VacacionSaldo.IdEmpleado = dbo.Empleado.Id
                GROUP BY dbo.VacacionSaldo.IdEmpleado, dbo.Empleado.NombreCompleto
                GO
                ");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TABLE EstadoVacacion");
            migrationBuilder.Sql(@"DROP TABLE RegistroVacacion");
            migrationBuilder.Sql(@"DROP TABLE VacacionSaldo");
            migrationBuilder.Sql(@"DROP VIEW VistaRegistroVacaciones");
            migrationBuilder.Sql(@"DROP VIEW VistaVacacionesConsolidadas");
        }
    }
}
