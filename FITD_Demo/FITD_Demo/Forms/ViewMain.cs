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
//
using ComponentFactory.Krypton.Toolkit;

namespace FITD_Demo.Forms
{
    public partial class ViewMain : KryptonForm
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
            RotacionCartera();
            PasivoCapital();
        }
 
        #region Razones de Liquidez
        
        public void CapitalTrabajo()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";            
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());            
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_CapitalTrabajo '" + repID + "'", cmd);
            //parametro 1 debe asignarle al numero de reporte al que se agregue
            //Fijar un metodo de menu de gestion para distintos reportes....
            //query.Parameters.AddWithValue("@ReportID", reportID);
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            //Usamos el SP de Capital de trabajo ya que para esta razon ocupamos los mismo datos
            SqlCommand query = new SqlCommand("EXEC SP_CapitalTrabajo '" + repID + "'", cmd);
                
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_PruebaAcida '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string AC = record["ActivoCorriente"].ToString();
                string Inv = record["Inventario"].ToString();
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_RotacionInventario '" + repID + "'", cmd);
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
                    decimal rotacionInventario = (costosMV / inventarios);
                    if(rotacionInventario > 0)
                    {
                        decimal rotacionInventarioMeses = (12 / rotacionInventario);
                        txtRotacionInv.Text = rotacionInventarioMeses.ToString("0.00");
                    }
                    else { MessageBox.Show("Debe tener una rotacion de Inventarios mayor a cero para continuar"); }
                }
                else { MessageBox.Show("Debe tener un total de Inventarios mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        public void RotacionCartera()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_RotacionCartera '" + repID + "'", cmd);
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_RotacionCuentasPagarCP '" + repID + "'", cmd);
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_RazonEndeudamiento '" + repID + "'", cmd);
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
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_PasivoCapital '" + repID + "'", cmd);
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

        public void MBU()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.ReportID) AS ReportID FROM Report AS R";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_PasivoCapital '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string V = record["Ventas"].ToString();
                string C = record["Costos"].ToString();

                double ventas = Convert.ToDouble(V);
                double costos = Convert.ToDouble(C);
                if (ventas > 0)
                {
                    double MBU = ((ventas - costos) / ventas);
                    //txtMBU.Text = MBU.ToString();
                }
                else { MessageBox.Show("Debe tener Ventas mayor a cero para continuar"); }

                cmd.Close();
            }
        }
    }
}
