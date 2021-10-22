create database FITD
use FITD

Create table Liquidez(
LiquidezID int primary key identity (1, 1) not null,
ActivoCorriente int null,
PasivoCirculante int null,
InventarioInicial int null,
InventarioFinal int null,
CostosMercanciasVendidas int null,
VentasCredito int null,
CuentasPorCobrarInicial int null,
CuentasPorCobrarFinal int null,
CuentasPorPagarInicial int null,
CuentasPorPagarFinal int null,
ComprasCredito int null)

select * from Liquidez

Create table Endeudamiento(
EndeudamientoID int primary key identity(1, 1) not null,
ActivoTotal int null,
PasivoTotal int null,
PasivoLargoPlazo int null,
Capital int null)

select * from Endeudamiento


Create table Rentabilidad(
RentabilidadID int primary key identity (1, 1) not null,
Ventas int null,
Costos int null,
TotalActivos int null,
UtilidadNeta int null)

select * from Rentabilidad

/*Agregando la tabla reporte*/
Create table Report(
ReportID int identity (1, 1) not null,
Nombre nvarchar(50) null,
LiquidezID int null,
EndeudamientoID int null,
RentabilidadID int null)


/*Agregando las llaves foraneas y relaciones*/
Alter table Report
add foreign key (LiquidezID) References Liquidez(LiquidezID)

Alter table Report
add foreign key (EndeudamientoID) References Endeudamiento(EndeudamientoID)

Alter table Report
add foreign key (RentabilidadID) References Rentabilidad(RentabilidadID)

/*Agregando algunos procesos de almacenados para el correcto funcionamiento de las peticiones por parte del programa FITD y mitigacion de errores en el query de la misma*/


/*Procedimiento de almacenado para calcular el Margen Bruto de Utilidad*/
Create PROCEDURE SP_MBU @ReportID int
AS
select ((R.Ventas - R.Costos)/R.Ventas) as MBU from Rentabilidad as R inner join Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.ReportID = @ReportID

EXEC SP_MBU 1


SELECT MAX(RR.LiquidezID) as LiquidezID, MAX(RR.EndeudamientoID) as EndeudamientoID, MAX(RR.RentabilidadID) as RentabilidadID  from Report as RR