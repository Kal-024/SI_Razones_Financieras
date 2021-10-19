using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FITD_Demo.Forms
{
    public partial class ViewMain : Form
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8; Initial Catalog = FITD; Integrated Security = true");

        public ViewMain()
        {
            InitializeComponent();
            RotacionCuentasPagarCP();
            CapitalTrabajo();
            IndiceSolvencia();
            PruebaAcida();RotacionInventarios();RotacionCuentasPagarCP();
            RazonEndeudamiento();
            PasivoCapital();
        }

        #region Razones de Liquidez
        public void CapitalTrabajo()
        {
            SqlCommand query = new SqlCommand("SELECT L.ActivoCorriente, L.PasivoCirculante FROM Liquidez as L WHERE LiquidezID = @ReportID", cmd);
            //parametro 1 debe asignarle al numero de reporte al que se agregue
            //Fijar un metodo de menu de gestion para distintos reportes....
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string AC = record["ActivoCorriente"].ToString();
                string PC = record["PasivoCirculante"].ToString();

                int activoCirculante = int.Parse(AC);
                int pasivoCirculante = int.Parse(PC);

                decimal capitalTrabajo = (activoCirculante - pasivoCirculante);
                txtCapitalT.Text = capitalTrabajo.ToString("0.00"); //textBox para imprimir resultado
                
            }
            cmd.Close();
        }

        public void IndiceSolvencia()
        {
            
                SqlCommand query = new SqlCommand("SELECT L.ActivoCorriente, L.PasivoCirculante FROM Liquidez as L WHERE LiquidezID = @ReportID", cmd);
                query.Parameters.AddWithValue("@ReportID", 1);
                cmd.Open();

                SqlDataReader record = query.ExecuteReader();
                if (record.Read())
                {
                    string AC = record["ActivoCorriente"].ToString();
                    string PC = record["PasivoCirculante"].ToString();

                    int activoCirculante = int.Parse(AC);
                    int pasivoCirculante = int.Parse(PC);
                    //validacion del pasivo mayor a cero
                    if (pasivoCirculante > 0)
                    {
                        decimal razonCorriente = (activoCirculante / pasivoCirculante);
                        txtSolvencia.Text = razonCorriente.ToString("0.00"); //textBox para imprimir resultado
                    }
                    else { MessageBox.Show("Debe tener un Pasivo Circulante mayor a cero para Continuar"); }
                }

                cmd.Close();
        }

        public void PruebaAcida()
        {
            SqlCommand query = new SqlCommand("SELECT L.ActivoCorriente,((L.InventarioInicial + L.InventarioFinal) / 2) as Inventarios , L.PasivoCirculante from Liquidez as L WHERE LiquidezID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string AC = record["ActivoCorriente"].ToString();
                string Inv = record["Inventarios"].ToString();
                string PC = record["PasivoCirculante"].ToString();

                int activoCirculante = int.Parse(AC);
                int inventarios = int.Parse(Inv);
                int pasivoCirculante = int.Parse(PC);

                if (pasivoCirculante > 0)
                {
                    decimal pruebaAcida = (activoCirculante - inventarios) / pasivoCirculante;
                    txtPruebaA.Text = pruebaAcida.ToString("0.00");
                }
                else { MessageBox.Show("Debe tener un Pasivo Circualante mayor a cero"); }
            }
            cmd.Close();
        }

        public void RotacionInventarios()
        {
            SqlCommand query = new SqlCommand("SELECT L.CostosMercanciasVendidas, ((L.InventarioInicial + L.InventarioFinal) / 2) as Inventarios FROM Liquidez as L WHERE LiquidezID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string CMV = record["CostosMercanciasVendidas"].ToString();
                string Inv = record["Inventarios"].ToString();

                int costosMV = int.Parse(CMV);
                int inventarios = int.Parse(Inv);
                if (inventarios > 0)
                {
                    //La respuesta da en Meses por lo tanto se divide entre los 12 meses hábiles...
                    decimal rotacionInventario = (12 / (costosMV / inventarios));
                    txtRotacionInv.Text = rotacionInventario.ToString("0.00");
                }else { MessageBox.Show("Debe tener un total de Inventarios mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        public void RotacionCartera()
        {
            SqlCommand query = new SqlCommand("SELECT L.VentasCredito, ((L.CuentasPorCobrarInicial + L.CuentasPorCobrarFinal) / 2) as PromedioCuentasCobrar FROM Liquidez as L WHERE LiquidezID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string VC = record["VentasCredito"].ToString();
                string PCC = record["PromedioCuentasCobrar"].ToString();

                int ventasCredito = int.Parse(VC);
                int promedioCuentasCobrar = int.Parse(PCC);

                if (promedioCuentasCobrar > 0)
                {
                    //La respuesta es en dia por lo tanto lo dividimo entre los 360 dias que tiene un año fiscal
                    decimal rotacionCartera = (ventasCredito / promedioCuentasCobrar);
                    if (rotacionCartera > 0)
                    {
                        decimal rotacionCarteraDias = (360 / rotacionCartera);
                        txtRotacionC.Text = rotacionCarteraDias.ToString("0.00");
                    }
                    else { MessageBox.Show("Su rotacion de Cartera fue de exactamente cero, Revise correctamente su Informacion!"); }
                    
                }
                else { MessageBox.Show("Debe tener un Promedio de Cuentas por Cobrar mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        public void RotacionCuentasPagarCP()
        {
            SqlCommand query = new SqlCommand("SELECT L.ComprasCredito, ((L.CuentasPorPagarInicial + L.CuentasPorPagarFinal) / 2) as PromedioCuentasPagar FROM Liquidez as L WHERE LiquidezID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string CC = record["ComprasCredito"].ToString();
                string PCP = record["PromedioCuentasPagar"].ToString();

                int comprasCredito = int.Parse(CC);
                int promedioCuentasPagar = int.Parse(PCP);

                if (promedioCuentasPagar > 0)
                {
                    //La respuesta es en dia por lo tanto lo dividimo entre los 360 dias que tiene un año fiscal
                    decimal rotacionCuentasPagarCP = (comprasCredito / promedioCuentasPagar);
                    if (rotacionCuentasPagarCP > 0)
                    {
                        decimal rotacionCuentasPagarCPDias = (360 / rotacionCuentasPagarCP);
                        txtRotacionPagarCP.Text = rotacionCuentasPagarCPDias.ToString("0.00");
                    }
                    else { MessageBox.Show("Su rotacion de Cuentas por pagar a Corto Plazo fue de exactamente cero, Revise correctamente su Informacion!"); }

                }
                else { MessageBox.Show("Debe tener un Promedio de Cuentas por Pagar mayor a cero para continuar"); }
            }
            cmd.Close();
        }
        #endregion

        #region Razones de Endeudamiento
        public void RazonEndeudamiento()
        {
            SqlCommand query = new SqlCommand("SELECT D.PasivoTotal, D.ActivoTotal FROM Endeudamiento as D WHERE EndeudamientoID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string PT = record["PasivoTotal"].ToString();
                string AT = record["ActivoTotal"].ToString();

                int pasivoTotal = int.Parse(PT);
                int activoTotal = int.Parse(AT);
                if (activoTotal > 0)
                {
                    decimal razonEndeudamientoPorcentual = (pasivoTotal / activoTotal) * (100);
                    txtRazonE.Text = razonEndeudamientoPorcentual.ToString("0.00");
                }
                else { MessageBox.Show("Sus Activos Totales deben ser mayores a cero para continuar"); }
            }
            cmd.Close();
        }

        public void PasivoCapital()
        {
            SqlCommand query = new SqlCommand("SELECT D.PasivoLargoPlazo, D.Capital FROM Endeudamiento as D WHERE EndeudamientoID = @ReportID", cmd);
            query.Parameters.AddWithValue("@ReportID", 1);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string PLP = record["PasivoLargoPlazo"].ToString();
                string C = record["Capital"].ToString();

                int pasivoLargoPlazo = int.Parse(PLP);
                int capital = int.Parse(C);
                if (capital > 0)
                {
                    decimal razonPasivoCapital = (pasivoLargoPlazo / capital);
                    txtRazonPC.Text = razonPasivoCapital.ToString("0.00");
                }
                else { MessageBox.Show("Su Capital debe ser mayor a cero para continuar"); }
            }
            cmd.Close();
        }


        #endregion
    }
}
