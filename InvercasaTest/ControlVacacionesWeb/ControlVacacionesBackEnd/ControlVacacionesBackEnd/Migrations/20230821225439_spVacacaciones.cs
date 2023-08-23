using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class spVacacaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[spInsRegistroVacacion]
	                -- Add the parameters for the stored procedure here

                @IdEmpleado int,
                @FechaVacacion Date,
                @IdEstadoVacacion int


                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;

                    -- Insert statements for procedure here
                INSERT INTO [dbo].[RegistroVacacion]
                           ([IdEmpleado]
                           ,[FechaVacacion]
		                   ,[IdEstadoVacacion])
                     VALUES
                           (@IdEmpleado,
			                @FechaVacacion,
			                @IdEstadoVacacion)

	                --SELECT @Id = SCOPE_IDENTITY();
                END
                GO
                ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[spInsRegistroVacacionYsaldos]
	                @IdEmpleado int,
	                @FechaVacacion Date,
	                @IdEstadoVacacion int,
	                @Ano int,
	                @Mes int,
	                @VacacionAcumuladaMes decimal(18,2) = 2.5,
	                @VacacionesTomadas decimal(18,2) = 1.0
	

                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;
	
	                SET @Ano = year(@FechaVacacion)
	                SET @Mes = month(@FechaVacacion)

	
	                --INSERT INTO VacacionSaldo (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);

	                IF @IdEstadoVacacion = 2 --ESTADO DE VACACIONES SEA IGUAL A TOMADAS
	                BEGIN
		                IF
			                (SELECT COUNT(*) AS total from VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
			                BEGIN
				                BEGIN DISTRIBUTED TRANSACTION
					                SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) + 1;
					                INSERT INTO RegistroVacacion (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
					                UPDATE VacacionSaldo SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
				                COMMIT TRANSACTION
			                END
		                ELSE
			                BEGIN DISTRIBUTED TRANSACTION
				                INSERT INTO RegistroVacacion (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
				                INSERT INTO VacacionSaldo (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
			                COMMIT TRANSACTION
	                END
	                ELSE
		                BEGIN TRANSACTION
			                INSERT INTO RegistroVacacion (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
		                COMMIT TRANSACTION
                END");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[spUpdRegistroVacacionYsaldos]
	                @IdEmpleado int,
	                @FechaVacacion Date,
	                @IdEstadoVacacion int,
	                @Ano int,
	                @Mes int,
	                @VacacionAcumuladaMes decimal(18,2) = 2.5,
	                @VacacionesTomadas decimal(18,2) = 1.0
	

                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;
	
	                SET @Ano = year(@FechaVacacion)
	                SET @Mes = month(@FechaVacacion)

	
	                --INSERT INTO VacacionSaldo (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);

	                IF (SELECT IdEstadoVacacion From RegistroVacacion Where IdEmpleado = @IdEmpleado and FechaVacacion = @FechaVacacion) <> @IdEstadoVacacion
	                BEGIN	
		                IF @IdEstadoVacacion = 2 --ESTADO DE VACACIONES SEA IGUAL A TOMADAS
		                BEGIN
			                IF
				                (SELECT COUNT(*) AS total from VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
				                BEGIN
					                BEGIN DISTRIBUTED TRANSACTION
						                SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) + 1;
						                --INSERT INTO RegistroVacacion (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
						                UPDATE RegistroVacacion SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
						                UPDATE VacacionSaldo SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
					                COMMIT TRANSACTION
				                END
			                ELSE
				                BEGIN DISTRIBUTED TRANSACTION
					                UPDATE RegistroVacacion SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
					                INSERT INTO VacacionSaldo (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
				                COMMIT TRANSACTION
		                END
		                ELSE
			                BEGIN
				                IF
					                (SELECT COUNT(*) AS total from VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
					                BEGIN
						                BEGIN DISTRIBUTED TRANSACTION
							                SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionSaldo where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) - 1;
							                --INSERT INTO RegistroVacacion (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
							                UPDATE RegistroVacacion SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
							                UPDATE VacacionSaldo SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
						                COMMIT TRANSACTION
					                END
				                ELSE
					                BEGIN DISTRIBUTED TRANSACTION
						                UPDATE RegistroVacacion SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
						                --INSERT INTO VacacionSaldo (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
					                COMMIT TRANSACTION
			                END

	                END

                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE spInsRegistroVacacion");
            migrationBuilder.Sql("DROP PROCEDURE spInsRegistroVacacionYsaldos");
            migrationBuilder.Sql("DROP PROCEDURE spUpdRegistroVacacionYsaldos");
        }
    }
}
