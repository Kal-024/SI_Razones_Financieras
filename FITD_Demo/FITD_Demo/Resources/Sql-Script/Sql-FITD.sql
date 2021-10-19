create database FITD
use FITD

Create table Liquidez(
LiquidezID int identity (1, 1) not null,
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
EndeudamientoID  int identity(1, 1) not null,
ActivoTotal int null,
PasivoTotal int null,
PasivoLargoPlazo int null,
Capital int null)

select * from Endeudamiento


Create table Rentabilidad(
RentabilidadID int identity (1, 1) not null,
Ventas int null,
Costos int null,
TotalActivos int null,
UtilidadNeta int null)

select * from Rentabilidad