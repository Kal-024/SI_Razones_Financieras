--select MAX(R.ReportID) as ReportID from Report as R
--SELECT MAX(L.LiquidezID) as LiquidezID, MAX(E.EndeudamientoID) as EndeudamientoID, MAX(R.RentabilidadID) as RentabilidadID  from Report as RR inner join Liquidez as L on RR.ReportID = L.LiquidezID inner join Endeudamiento as E on RR.ReportID = E.EndeudamientoID inner join Rentabilidad as R on RR.ReportID = R.RentabilidadID
use master
drop database FITD
create database FITD
use FITD

Create table Liquidez(
LiquidezID int primary key identity (1, 1) not null,
ActivoCorriente int null,
PasivoCirculante int null,
Inventario int null,
InventarioInicial int null,
InventarioFinal int null,
CostosMercanciasVendidas int null,
VentasCredito int null,
CuentasPorCobrarInicial int null,
CuentasPorCobrarFinal int null,
CuentasPorPagarInicial int null,
CuentasPorPagarFinal int null,
ComprasCredito int null)

---select * from Liquidez

Create table Endeudamiento(
EndeudamientoID int primary key identity(1, 1) not null,
ActivoTotal int null,
PasivoTotal int null,
PasivoLargoPlazo int null,
Capital int null)

---select * from Endeudamiento


Create table Rentabilidad(
RentabilidadID int primary key identity (1, 1) not null,
Ventas int null,
Costos int null,
TotalActivos int null,
UtilidadNeta int null)

---select * from Rentabilidad

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

Create PROCEDURE SP_CapitalTrabajo @ReportID int as
SELECT DISTINCT L.ActivoCorriente, L.PasivoCirculante FROM Liquidez as L inner join Report as R on L.LiquidezID = R.LiquidezID WHERE R.ReportID = @ReportID group by L.ActivoCorriente,L.PasivoCirculante,R.LiquidezID

EXEC SP_CapitalTrabajo 3

Create PROCEDURE SP_PruebaAcida @ReportID int as
SELECT DISTINCT L.ActivoCorriente, L.Inventario , L.PasivoCirculante FROM Liquidez as L inner join Report as R on L.LiquidezID = R.LiquidezID where R.ReportID = @ReportID group by L.ActivoCorriente,l.PasivoCirculante,L.Inventario,R.LiquidezID

EXEC SP_PruebaAcida 1

Create PROCEDURE SP_RotacionInventario @ReportID int as
SELECT L.CostosMercanciasVendidas, ((L.InventarioInicial + L.InventarioFinal) / 2) as Inventarios FROM Liquidez as L inner join Report as R on L.LiquidezID = R.LiquidezID WHERE R.ReportID = @ReportID group by L.InventarioInicial,L.InventarioFinal,L.CostosMercanciasVendidas,R.LiquidezID

EXEC SP_RotacionInventario 1


Create PROCEDURE SP_RotacionCartera @ReportID int as
SELECT L.VentasCredito, ((L.CuentasPorCobrarInicial + L.CuentasPorCobrarFinal) / 2) as PromedioCuentasCobrar FROM Liquidez as L inner join Report as R on L.LiquidezID = R.LiquidezID WHERE R.ReportID = @ReportID group by L.VentasCredito,L.CuentasPorCobrarInicial,L.CuentasPorCobrarFinal,R.LiquidezID

EXEC SP_RotacionCartera 1


Create PROCEDURE SP_RotacionCuentasPagarCP @ReportID int as
SELECT L.ComprasCredito, ((L.CuentasPorPagarInicial + L.CuentasPorPagarFinal) / 2) as PromedioCuentasPagar FROM Liquidez as L inner join Report as R on L.LiquidezID = R.LiquidezID WHERE R.ReportID = @ReportID group by L.ComprasCredito,L.CuentasPorPagarInicial,L.CuentasPorPagarFinal,R.LiquidezID

EXEC SP_RotacionCuentasPagarCP 1



Create PROCEDURE SP_RazonEndeudamiento @ReportID int as
SELECT D.PasivoTotal, D.ActivoTotal FROM Endeudamiento as D inner join Report as R on D.EndeudamientoID = R.EndeudamientoID WHERE R.ReportID = @ReportID group by D.ActivoTotal,D.PasivoTotal,R.EndeudamientoID

EXEC SP_RazonEndeudamiento 1


Create PROCEDURE SP_PasivoCapital @ReportID int as
SELECT D.PasivoLargoPlazo, D.Capital FROM Endeudamiento as D inner join Report as R on D.EndeudamientoID = R.EndeudamientoID WHERE R.ReportID = @ReportID group by D.PasivoLargoPlazo,D.Capital,R.EndeudamientoID

EXEC SP_PasivoCapital 1


/*Procedimiento de almacenado para calcular el Margen Bruto de Utilidad*/
ALTER PROCEDURE SP_MBU @ReportID int as
select DISTINCT R.Ventas, R.Costos from Rentabilidad as R inner join Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.ReportID = @ReportID group by R.Ventas,R.Costos,RR.RentabilidadID

EXEC SP_MBU 1

select * from Rentabilidad






/*
insert into Liquidez values(45000000,18000000,25000000,13500000,20000000,87000000,455000000,13000000,9150000,2020000,850250,22750000)
insert into Endeudamiento values(98500,9000,35600,28000)
insert into Rentabilidad values(132000000,40000000,31500000,3200000)
insert into Report values('RazonFinancieraPrueba1',1,1,1)
*/