using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE spDelEmpleado
                        @Id int

                        AS
                            BEGIN
                                SET NOCOUNT ON;

                                DELETE FROM Empleado WHERE Id=@Id;
        
                            END");

            migrationBuilder.Sql(@"
                    CREATE PROCEDURE spInsEmpleado
                        @NombreCompleto nvarchar(max),
                        @IdTipoIdentificacion int,
                        @NumeroIdentificacion nvarchar(max),
                        @FechaIngreso date,
                        @SalarioBaseMensual decimal(18,2),
                        @Direccion nvarchar(max),
                        @Id int OUTPUT

                        AS
                            BEGIN
                                SET NOCOUNT ON;

                                INSERT INTO Empleado
                                    (NombreCompleto, IdTipoIdentificacion, NumeroIdentificacion, FechaIngreso
                                    ,SalarioBaseMensual,Direccion)
                                VALUES 
                                    (@NombreCompleto, @IdTipoIdentificacion, @NumeroIdentificacion,@FechaIngreso
                                     ,@SalarioBaseMensual, @Direccion);
        
                                SELECT @Id = SCOPE_IDENTITY();
                            END");

            migrationBuilder.Sql(@"
                    CREATE PROCEDURE spUpdEmpleado
                        @Id int,
                        @NombreCompleto nvarchar(max),
                        @IdTipoIdentificacion int,
                        @NumeroIdentificacion nvarchar(max),
                        @FechaIngreso date,
                        @SalarioBaseMensual decimal(18,2),
                        @Direccion nvarchar(max)

                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            
                            UPDATE Empleado SET 
                                NombreCompleto=@NombreCompleto, IdTipoIdentificacion=@IdTipoIdentificacion
                                , NumeroIdentificacion=@NumeroIdentificacion, FechaIngreso=@FechaIngreso
                                , SalarioBaseMensual=@SalarioBaseMensual, Direccion=@Direccion
                            WHERE Id=@Id;
                        END");

            migrationBuilder.Sql(@"
                    CREATE PROCEDURE spSelEmpleados
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            SELECT 
                                Id, NombreCompleto, IdTipoIdentificacion,NumeroIdentificacion
                                , FechaIngreso, SalarioBaseMensual, Direccion 
                            FROM Empleado 
                            ORDER BY NombreCompleto;
                        END");
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE spSelEmpleadoId
                        @Id int
                        AS
                        BEGIN
                            SET NOCOUNT ON;

                            SELECT
                                Id, NombreCompleto, IdTipoIdentificacion, NumeroIdentificacion, FechaIngreso, SalarioBaseMensual, Direccion
                            FROM Empleado
                            WHERE Id=@Id
                        END");












        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE spDelEmpleado");
            migrationBuilder.Sql("DROP PROCEDURE spInsEmpleado");
            migrationBuilder.Sql("DROP PROCEDURE spUpdEmpleado");
            migrationBuilder.Sql("DROP PROCEDURE spSelEmpleados");
            migrationBuilder.Sql("DROP PROCEDURE spSelEmpleadoId");

        }
    }
}
