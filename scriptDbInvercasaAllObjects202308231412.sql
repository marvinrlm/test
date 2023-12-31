USE [master]
GO
/****** Object:  Database [invercasa]    Script Date: 8/23/2023 2:19:36 PM ******/
CREATE DATABASE [invercasa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'invercasa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\invercasa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'invercasa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\invercasa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [invercasa] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [invercasa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [invercasa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [invercasa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [invercasa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [invercasa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [invercasa] SET ARITHABORT OFF 
GO
ALTER DATABASE [invercasa] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [invercasa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [invercasa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [invercasa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [invercasa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [invercasa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [invercasa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [invercasa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [invercasa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [invercasa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [invercasa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [invercasa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [invercasa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [invercasa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [invercasa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [invercasa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [invercasa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [invercasa] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [invercasa] SET  MULTI_USER 
GO
ALTER DATABASE [invercasa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [invercasa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [invercasa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [invercasa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [invercasa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [invercasa] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [invercasa] SET QUERY_STORE = ON
GO
ALTER DATABASE [invercasa] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [invercasa]
GO
/****** Object:  UserDefinedFunction [dbo].[fnCalcularVacaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnCalcularVacaciones]
(
	-- Add the parameters for the function here
	@FechaInicio DateTime, @FechaFin DateTime
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

	SELECT @DifAno = (SELECT DATEDIFF(YY,@FechaInicio,@FechaFin) AS ANOS) --obtengo la dif en anos
	SELECT @DifMes = (SELECT DATEDIFF(MM,@FechaInicio,@FechaFin) AS MESES) --obtengo la dif en mes
	SELECT @DifDia = (SELECT DATEDIFF(DD,@FechaInicio,@FechaFin) AS DIAS)+1; --obtengo la dif en dias
	--PRINT 'PASO'
	IF @DifMes < 1
	BEGIN
		IF @MesFin=2 and @DiaFin=28
		BEGIN
			SET  @DiasGenerados = 2.5
		END
		ELSE IF @MesFin<>2 and @DiaFin>30
		BEGIN
			SET  @DiasGenerados = 2.5
		END
		ELSE
		BEGIN
			SET  @DiasGenerados = @DifDia*@TASA
		END
	END
	ELSE IF @DifMes = 1
	BEGIN
		IF @MesFin=2 and @DiaFin=28
		BEGIN
			IF @DiaIni=31
			BEGIN
				SET  @DiasGenerados = (31*@TASA)
			END
			ELSE IF @DiaIni=30
			BEGIN
				SET  @DiasGenerados = (32*@TASA)
			END
			ELSE
			BEGIN
				SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (30*@TASA))
			END
		END
		ELSE IF @MesFin<>2 and @DiaFin>30
		BEGIN
			SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (30*@TASA))
		END
		ELSE
		BEGIN
			SET  @DiasGenerados = ((30-(@DiaIni -1))*@TASA  + (@DiaFin*@TASA))
		END
	END
	ELSE IF @DifMes > 1
	
	BEGIN
		IF @MesFin=2 and @DiaFin=28
		BEGIN
			IF @DiaIni=31
			BEGIN
				--SET  @DiasGenerados = (31*@TASA)
				set @DiasGenerados = (1*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA))
			END
			ELSE IF @DiaIni=30
			BEGIN
				--SET  @DiasGenerados = (32*@TASA)
				set @DiasGenerados = (2*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA))
			END
			ELSE
			BEGIN
				--SET  @DiasGenerados = ((30-(@DiaIni -2))*@TASA  + (30*@TASA))
				SET @DiasGenerados = ((30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA))
			END
		END
			--SET @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA)
		ELSE IF @MesFin<>2 and @DiaFin>30
		BEGIN
			set @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (30*@TASA)
		END
		ELSE
		BEGIN
			set @DiasGenerados = (30-(@DiaIni -1))*@TASA + ((@DifMes-1)*2.5)  + (@DiaFin*@TASA)
		END
	END

	
	--set @DiasGenerados=1
	RETURN @DiasGenerados

END
GO
/****** Object:  Table [dbo].[TiposIdentificaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposIdentificaciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoIdentificacion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TiposIdentificaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [nvarchar](max) NOT NULL,
	[IdTipoIdentificacion] [int] NOT NULL,
	[NumeroIdentificacion] [nvarchar](max) NOT NULL,
	[FechaIngreso] [date] NOT NULL,
	[SalarioBaseMensual] [decimal](18, 2) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vEmpleados]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vEmpleados]
AS
SELECT dbo.Empleados.Id, dbo.Empleados.NombreCompleto, dbo.TiposIdentificaciones.TipoIdentificacion, dbo.Empleados.NumeroIdentificacion, dbo.Empleados.FechaIngreso, dbo.Empleados.SalarioBaseMensual, dbo.Empleados.Direccion, 
                  dbo.Empleados.IdTipoIdentificacion
FROM     dbo.Empleados LEFT OUTER JOIN
                  dbo.TiposIdentificaciones ON dbo.TiposIdentificaciones.Id = dbo.Empleados.IdTipoIdentificacion
GO
/****** Object:  Table [dbo].[VacacionesSaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VacacionesSaldos](
	[IdEmpleado] [int] NOT NULL,
	[Ano] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[VacacionesAcumuladas] [decimal](18, 2) NOT NULL,
	[VacacionesTomadas] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VacacionesSaldos] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC,
	[Ano] ASC,
	[Mes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vVacacionesConsolidadas]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*GROUP BY VacacionesAcumuladas, VacacionesTomadas*/
CREATE VIEW [dbo].[vVacacionesConsolidadas]
AS
SELECT dbo.VacacionesSaldos.IdEmpleado, dbo.Empleados.NombreCompleto, SUM(dbo.VacacionesSaldos.VacacionesAcumuladas) AS Acumulado, SUM(dbo.VacacionesSaldos.VacacionesTomadas) AS Tomadas, 
                  SUM(dbo.VacacionesSaldos.VacacionesAcumuladas) - SUM(dbo.VacacionesSaldos.VacacionesTomadas) AS Saldo
FROM     dbo.VacacionesSaldos INNER JOIN
                  dbo.Empleados ON dbo.VacacionesSaldos.IdEmpleado = dbo.Empleados.Id
GROUP BY dbo.VacacionesSaldos.IdEmpleado, dbo.Empleados.NombreCompleto
GO
/****** Object:  Table [dbo].[EstadosVacaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadosVacaciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EstadoVacacion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EstadosVacaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistrosVacaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrosVacaciones](
	[IdEmpleado] [int] NOT NULL,
	[FechaVacacion] [date] NOT NULL,
	[IdEstadoVacacion] [int] NOT NULL,
 CONSTRAINT [PK_RegistrosVacaciones] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC,
	[FechaVacacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vRegistroVacaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vRegistroVacaciones]
AS
SELECT dbo.RegistrosVacaciones.IdEmpleado, dbo.Empleados.NombreCompleto, dbo.RegistrosVacaciones.FechaVacacion, dbo.EstadosVacaciones.EstadoVacacion
FROM     dbo.Empleados INNER JOIN
                  dbo.RegistrosVacaciones ON dbo.Empleados.Id = dbo.RegistrosVacaciones.IdEmpleado INNER JOIN
                  dbo.EstadosVacaciones ON dbo.RegistrosVacaciones.IdEstadoVacacion = dbo.EstadosVacaciones.Id
GO
/****** Object:  Table [dbo].[Feriados]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feriados](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[DiaFeriado] [date] NOT NULL,
 CONSTRAINT [PK_Feriados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Feriados] UNIQUE NONCLUSTERED 
(
	[DiaFeriado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD  CONSTRAINT [FK_Empleados_TiposIdentificaciones] FOREIGN KEY([IdTipoIdentificacion])
REFERENCES [dbo].[TiposIdentificaciones] ([Id])
GO
ALTER TABLE [dbo].[Empleados] CHECK CONSTRAINT [FK_Empleados_TiposIdentificaciones]
GO
ALTER TABLE [dbo].[RegistrosVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_RegistrosVacaciones_EstadosVacaciones] FOREIGN KEY([IdEstadoVacacion])
REFERENCES [dbo].[EstadosVacaciones] ([Id])
GO
ALTER TABLE [dbo].[RegistrosVacaciones] CHECK CONSTRAINT [FK_RegistrosVacaciones_EstadosVacaciones]
GO
/****** Object:  StoredProcedure [dbo].[spDelEmpleado]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDelEmpleado]
	-- Add the parameters for the stored procedure here


@Id int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DELETE FROM Empleados
	 WHERE Id=@Id;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spInsEmpleado]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsEmpleado]
	-- Add the parameters for the stored procedure here



@NombreCompleto nvarchar(max),
@IdTipoIdentificacion int,
@NumeroIdentificacion nvarchar(max),
@FechaIngreso date,
@SalarioBaseMensual decimal(18,2),
@Direccion nvarchar(max),
@Id int OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[Empleados]
           ([NombreCompleto]
           ,[IdTipoIdentificacion]
           ,[NumeroIdentificacion]
           ,[FechaIngreso]
           ,[SalarioBaseMensual]
           ,[Direccion])
     VALUES
           (@NombreCompleto,
			@IdTipoIdentificacion,
			@NumeroIdentificacion,
			@FechaIngreso,
			@SalarioBaseMensual,
			@Direccion)

	SELECT @Id = SCOPE_IDENTITY();




END
GO
/****** Object:  StoredProcedure [dbo].[spInsRegistroVacacion]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
INSERT INTO [dbo].[RegistrosVacaciones]
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
/****** Object:  StoredProcedure [dbo].[spInsRegistroVacacionYsaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

	
	--INSERT INTO VacacionesSaldos (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);

	IF @IdEstadoVacacion = 2 --ESTADO DE VACACIONES SEA IGUAL A TOMADAS
	BEGIN
		IF
			(SELECT COUNT(*) AS total from VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
			BEGIN
				BEGIN DISTRIBUTED TRANSACTION
					SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) + 1;
					INSERT INTO RegistrosVacaciones (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
					UPDATE VacacionesSaldos SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN DISTRIBUTED TRANSACTION
				INSERT INTO RegistrosVacaciones (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
				INSERT INTO VacacionesSaldos (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
			COMMIT TRANSACTION
	END
	ELSE
		BEGIN TRANSACTION
			INSERT INTO RegistrosVacaciones (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
		COMMIT TRANSACTION
END
GO
/****** Object:  StoredProcedure [dbo].[spInsVacacionesSaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsVacacionesSaldos]
	-- Add the parameters for the stored procedure here


@IdEmpleado int,
@Ano int,
@Mes int,
@VacacionesAcumuladas decimal(18,2),
@VacacionesTomadas decimal(18,2)





AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

INSERT INTO [dbo].[VacacionesSaldos]
           ([IdEmpleado]
           ,[Ano]
           ,[Mes]
           ,[VacacionesAcumuladas]
           ,[VacacionesTomadas])
     VALUES
           (@IdEmpleado
           ,@Ano
           ,@Mes
           ,@VacacionesAcumuladas
           ,@VacacionesTomadas)

	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spSelEmpleadoId]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelEmpleadoId]
	-- Add the parameters for the stored procedure here

	@Id int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT     Id,
           NombreCompleto
           ,IdTipoIdentificacion
           ,NumeroIdentificacion
           ,FechaIngreso
           ,SalarioBaseMensual
           ,Direccion
    FROM [dbo].[Empleados]
	where Id=@Id;





END
GO
/****** Object:  StoredProcedure [dbo].[spSelEmpleados]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelEmpleados]
	-- Add the parameters for the stored procedure here




AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT     Id,
           NombreCompleto
           ,IdTipoIdentificacion
           ,NumeroIdentificacion
           ,FechaIngreso
           ,SalarioBaseMensual
           ,Direccion
    FROM [dbo].[Empleados]





END
GO
/****** Object:  StoredProcedure [dbo].[spSelEstadoVacacion]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelEstadoVacacion]
	-- Add the parameters for the stored procedure here




AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id,EstadoVacacion
    FROM [dbo].EstadosVacaciones
	ORDER BY EstadoVacacion





END
GO
/****** Object:  StoredProcedure [dbo].[spSelRegistroVacacionesId]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelRegistroVacacionesId]
	-- Add the parameters for the stored procedure here

	@IdEmpleado int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdEmpleado,FechaVacacion,IdEstadoVacacion
    FROM [dbo].RegistrosVacaciones
	where IdEmpleado=@IdEmpleado
	ORDER BY FechaVacacion





END
GO
/****** Object:  StoredProcedure [dbo].[spSelTipoIdentificacion]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelTipoIdentificacion]
	-- Add the parameters for the stored procedure here




AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id,TipoIdentificacion
    FROM [dbo].TiposIdentificaciones
	ORDER BY TipoIdentificacion





END
GO
/****** Object:  StoredProcedure [dbo].[spSelVacacionesSaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelVacacionesSaldos]
	-- Add the parameters for the stored procedure here

/*
@IdEmpleado int,
@Ano int,
@Mes int
*/



AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select IdEMpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas 
	FROM [dbo].[VacacionesSaldos] 
	 --WHERE IdEmpleado=@IdEmpleado and Ano=@Ano and Mes=@Mes;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spSelVacacionesSaldosId]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelVacacionesSaldosId]
	-- Add the parameters for the stored procedure here


@IdEmpleado int,
@Ano int,
@Mes int




AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select IdEMpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas 
	FROM [dbo].[VacacionesSaldos] 
	 WHERE IdEmpleado=@IdEmpleado and Ano=@Ano and Mes=@Mes;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spSelvEmpleados]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelvEmpleados]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Id,NombreCompleto, TipoIdentificacion, NumeroIdentificacion, FechaIngreso, SalarioBaseMensual, Direccion, IdTipoIdentificacion FROM dbo.vEmpleados
	ORDER BY Id
END
GO
/****** Object:  StoredProcedure [dbo].[spSelvRegistroVacacionesId]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelvRegistroVacacionesId]
	-- Add the parameters for the stored procedure here

	@IdEmpleado int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdEmpleado,NombreCompleto,FechaVacacion,EstadoVacacion
    FROM vRegistroVacaciones
	where IdEmpleado=@IdEmpleado
	ORDER BY FechaVacacion





END
GO
/****** Object:  StoredProcedure [dbo].[spSelvVacacionesConsolidadas]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSelvVacacionesConsolidadas] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [IdEmpleado]
      ,[NombreCompleto]
      ,[Acumulado]
      ,[Tomadas]
      ,[Saldo]
  FROM [dbo].[vVacacionesConsolidadas]



END
GO
/****** Object:  StoredProcedure [dbo].[spUpdEmpleado]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdEmpleado]
	-- Add the parameters for the stored procedure here


@Id int,
@NombreCompleto nvarchar(max),
@IdTipoIdentificacion int,
@NumeroIdentificacion nvarchar(max),
@FechaIngreso date,
@SalarioBaseMensual decimal(18,2),
@Direccion nvarchar(max)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[Empleados]
	   SET [NombreCompleto] = @NombreCompleto,
		   [IdTipoIdentificacion] = @IdTipoIdentificacion,
		   [NumeroIdentificacion] = @NumeroIdentificacion,
		   [FechaIngreso] = @FechaIngreso,
		   [SalarioBaseMensual] = @SalarioBaseMensual,
		   [Direccion] = @Direccion
	 WHERE Id=@Id;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdRegistroVacaciones]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdRegistroVacaciones]
	-- Add the parameters for the stored procedure here


@IdEmpleado int,
@FechaVacacion date,
@IdEstadoVacacion int




AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[RegistrosVacaciones]
	   SET [IdEstadoVacacion] = @IdEstadoVacacion
	 WHERE IdEmpleado=@IdEmpleado and FechaVacacion=@FechaVacacion;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdRegistroVacacionYsaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

	
	--INSERT INTO VacacionesSaldos (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);

	IF (SELECT IdEstadoVacacion From RegistrosVacaciones Where IdEmpleado = @IdEmpleado and FechaVacacion = @FechaVacacion) <> @IdEstadoVacacion
	BEGIN	
		IF @IdEstadoVacacion = 2 --ESTADO DE VACACIONES SEA IGUAL A TOMADAS
		BEGIN
			IF
				(SELECT COUNT(*) AS total from VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
				BEGIN
					BEGIN DISTRIBUTED TRANSACTION
						SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) + 1;
						--INSERT INTO RegistrosVacaciones (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
						UPDATE RegistrosVacaciones SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
						UPDATE VacacionesSaldos SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
					COMMIT TRANSACTION
				END
			ELSE
				BEGIN DISTRIBUTED TRANSACTION
					UPDATE RegistrosVacaciones SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
					INSERT INTO VacacionesSaldos (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
				COMMIT TRANSACTION
		END
		ELSE
			BEGIN
				IF
					(SELECT COUNT(*) AS total from VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes)=1
					BEGIN
						BEGIN DISTRIBUTED TRANSACTION
							SELECT @VacacionesTomadas = (SELECT VacacionesTomadas From VacacionesSaldos where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes) - 1;
							--INSERT INTO RegistrosVacaciones (IdEmpleado, FechaVacacion, IdEstadoVacacion) VALUES (@IdEmpleado, @FechaVacacion, @IdEstadoVacacion);
							UPDATE RegistrosVacaciones SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
							UPDATE VacacionesSaldos SET VacacionesTomadas = @VacacionesTomadas  where IdEmpleado = @IdEmpleado and Ano=@Ano and Mes=@Mes;
						COMMIT TRANSACTION
					END
				ELSE
					BEGIN DISTRIBUTED TRANSACTION
						UPDATE RegistrosVacaciones SET IdEstadoVacacion = @IdEstadoVacacion WHERE IdEmpleado = @IdEmpleado AND FechaVacacion =@FechaVacacion;
						--INSERT INTO VacacionesSaldos (IdEmpleado, Ano, Mes, VacacionesAcumuladas, VacacionesTomadas) VALUES (@IdEmpleado, @Ano, @Mes, @VacacionAcumuladaMes, 1);
					COMMIT TRANSACTION
			END

	END

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdVacacionesSaldos]    Script Date: 8/23/2023 2:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdVacacionesSaldos]
	-- Add the parameters for the stored procedure here


@IdEmpleado int,
@Ano int,
@Mes int,
--@VacacionesAcumuladas decimal(18,2),
@VacacionesTomadas decimal(18,2)





AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[VacacionesSaldos]
   SET [IdEmpleado] = @IdEmpleado
      ,[Ano] = @Ano
      ,[Mes] = @Mes
      --,[VacacionesAcumuladas] = @VacacionesAcumuladas
      ,[VacacionesTomadas] = @VacacionesTomadas
	 WHERE IdEmpleado=@IdEmpleado and Ano=@Ano and Mes=@Mes;
	--SELECT @Id = SCOPE_IDENTITY();

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "TiposIdentificaciones"
            Begin Extent = 
               Top = 0
               Left = 799
               Bottom = 119
               Right = 1014
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmpleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[3] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EstadosVacaciones"
            Begin Extent = 
               Top = 49
               Left = 819
               Bottom = 177
               Right = 1019
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RegistrosVacaciones"
            Begin Extent = 
               Top = 93
               Left = 452
               Bottom = 234
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vRegistroVacaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vRegistroVacaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[29] 4[11] 2[25] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "VacacionesSaldos"
            Begin Extent = 
               Top = 34
               Left = 288
               Bottom = 249
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Empleados"
            Begin Extent = 
               Top = 42
               Left = 664
               Bottom = 205
               Right = 903
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1632
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vVacacionesConsolidadas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vVacacionesConsolidadas'
GO
USE [master]
GO
ALTER DATABASE [invercasa] SET  READ_WRITE 
GO
