using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FuncionesEscalares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
								CREATE FUNCTION fnCalcularVacaciones
								(
									-- Add the parameters for the function here
									@FechaInicio Date, @FechaFin Date
								)
								RETURNS decimal(18,4)
								AS
								BEGIN
									-- Declare the return variable here
									DECLARE @DiasGenerados decimal(18,4) --<@ResultVar, sysname, @Result> <Function_Data_Type, ,int>
									--CALCULO DE LA TASA SEGUN LEY 
									DECLARE @TASA decimal(18,10);
									DECLARE @DIASMESACUMULADO decimal(18,4);
									SET @DIASMESACUMULADO = 2.5;
									DECLARE @CANTDIASMES int;
									SET @CANTDIASMES = 30;
	
									SET @TASA = @DIASMESACUMULADO/@CANTDIASMES;


									--OBTENER LOS DIAS, MESES, ANOS SEPARADOS PARA LOS DOS PARAMETROS
									DECLARE @AnoIni int, @MesIni int, @DiaIni int;
									DECLARE @AnoFin int, @MesFin int, @DiaFin int;
									DECLARE @DifAno int, @DifMes int, @DifDia int;

									--DECLARE @DifDiasIni int, @DifDiasFin int;
									DECLARE @DiaFinHelp int;

									SELECT @AnoIni = (SELECT YEAR(@FechaInicio));  --obtengo el ano de inicio
									SELECT @MesIni = (SELECT MONTH(@FechaInicio)); --obtengo el mes de inicio
									SELECT @DiaIni = (SELECT DAY(@FechaInicio));   --obtengo el dia de inicio 

									SELECT @AnoFin = (SELECT YEAR(@FechaFin));     --obtengo el dia de fin
									SELECT @MesFin = (SELECT MONTH(@FechaFin));    --obtengo el mes de fin
									SELECT @DiaFin = (SELECT DAY(@FechaFin));      --obtengo el ano de fin

									SELECT @DifAno = (SELECT DATEDIFF(YY,@FechaInicio,@FechaFin) AS DIAS) --obtengo la dif en anos
									SELECT @DifMes = (SELECT DATEDIFF(MM,@FechaInicio,@FechaFin) AS DIAS) --obtengo la dif en mes
									SELECT @DifDia = (SELECT DATEDIFF(DD,@FechaInicio,@FechaFin) AS DIAS)+1; --obtengo la dif en dias

									if @DifMes < 1
										BEGIN
											if @MesFin=2 and @DiaFin=28
												SET  @DiasGenerados = 2.5
											else if @MesFin<>2 and @DiaFin>30
												SET  @DiasGenerados = 2.5
											else
												SET  @DiasGenerados = @DifDia*@TASA
										END
	

									if @DifMes =1
										BEGIN
										if @MesFin=2 and @DiaFin=28
											BEGIN
												IF @DiaIni=31
													SET  @DiasGenerados = (31*@TASA)
												ELSE IF @DiaIni=30
													SET  @DiasGenerados = (32*@TASA)
												ELSE
													SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (30*@TASA))
											END
										else if @MesFin<>2 and @DiaFin>30
											SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (30*@TASA))
										else
											SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (@DiaFin*@TASA))
										END

									if @DifMes > 1
										BEGIN
										if @MesFin=2 and @DiaFin=28
											BEGIN
												IF @DiaIni=31
													--SET  @DiasGenerados = (31*@TASA)
													set @DiasGenerados = (1*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA))
												ELSE IF @DiaIni=30
													--SET  @DiasGenerados = (32*@TASA)
													set @DiasGenerados = (2*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA))
												ELSE
													--SET  @DiasGenerados = ((30-(@DiaIni -2))*@TASA  + (30*@TASA))
													SET @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA)
											END




											--SET @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA)
										else if @MesFin<>2 and @DiaFin>30
											set @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA)
										else
											set @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (@DiaFin*@TASA)
										END

									RETURN @DiasGenerados

								END
								GO

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP FUNCTION fnCalcularVacaciones");

        }
    }
}
