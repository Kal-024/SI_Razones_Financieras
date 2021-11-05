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
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION; Initial Catalog = FITD; Integrated Security = true");

        public ViewMain(int reportID)
        {
            this.reportID = reportID;
            InitializeComponent();
            //Razones de Liquidez
            CapitalTrabajo();
            IndiceSolvencia();
            PruebaAcida();
            RotacionInventarios();RotacionCartera();RotacionCuentasPagarCP();
            //Razones de Endeudamiento
            RazonEndeudamiento();
            PasivoCapital();
            //Razones de Rentabilidad
            MBU();
            MUP();
            RotacionActivoALP();
            ROA();
        }

        int reportID;
 
        #region Razones de Liquidez
        
        public void CapitalTrabajo()
        {
            cmd.Open();
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());            
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_CapitalTrabajo '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string AC = record["ActivoCorriente"].ToString();
                string PC = record["PasivoCirculante"].ToString();

                double activoCirculante = Convert.ToDouble(AC);
                double pasivoCirculante = Convert.ToDouble(PC);

                double capitalTrabajo = (activoCirculante - pasivoCirculante);
                txtCapitalT.Text = Math.Round(capitalTrabajo,4).ToString("0.00"); //textBox para imprimir resultado
                
            }
            cmd.Close();
        }

        public void IndiceSolvencia()
        {
            cmd.Open();
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                    double activoCirculante = Convert.ToDouble(AC);
                    double pasivoCirculante = Convert.ToDouble(PC);
                    //validacion del pasivo mayor a cero
                    if (pasivoCirculante > 0)
                    {
                        double razonCorriente = (activoCirculante / pasivoCirculante);
                        txtSolvencia.Text = Math.Round(razonCorriente,4).ToString("0.00"); //textBox para imprimir resultado
                    }
                    else { MessageBox.Show("Debe tener un Pasivo Circulante mayor a cero para Continuar"); }
                }

                cmd.Close();
        }

        public void PruebaAcida()
        {
            cmd.Open();
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double activoCirculante = Convert.ToDouble(AC);
                double inventarios = Convert.ToDouble(Inv);
                double pasivoCirculante = Convert.ToDouble(PC);

                if (pasivoCirculante > 0)
                {
                    double pruebaAcida = (activoCirculante - inventarios) / pasivoCirculante;
                    txtPruebaA.Text = Math.Round(pruebaAcida,4).ToString("0.00");
                }
                else { MessageBox.Show("Debe tener un Pasivo Circualante mayor a cero"); }
            }
            cmd.Close();
        }

        public void RotacionInventarios()
        {
            cmd.Open();
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double costosMV = Convert.ToDouble(CMV);
                double inventarios = Convert.ToDouble(Inv);
                if (inventarios > 0)
                {
                    //La respuesta da en Meses por lo tanto se divide entre los 12 meses hábiles...
                    double rotacionInventario = (costosMV / inventarios);
                    if(rotacionInventario > 0)
                    {
                        double rotacionInventarioMeses = (12 / rotacionInventario);
                        txtRotacionInv.Text = Math.Round(rotacionInventarioMeses,1).ToString("0.00");
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
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double ventasCredito = Convert.ToDouble(VC);
                double promedioCuentasCobrar = Convert.ToDouble(PCC);

                if (promedioCuentasCobrar > 0)
                {
                    //La respuesta es en dia por lo tanto lo dividimo entre los 360 dias que tiene un año fiscal
                    double rotacionCartera = (ventasCredito / promedioCuentasCobrar);
                    if (rotacionCartera > 0)
                    {
                        double rotacionCarteraDias = (360 / rotacionCartera);
                        txtRotacionC.Text = Math.Round(rotacionCarteraDias).ToString("0.00");
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
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L INNER JOIN Report as RR on L.LiquidezID = RR.LiquidezID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double comprasCredito = Convert.ToDouble(CC);
                double promedioCuentasPagar = Convert.ToDouble(PCP);

                if (promedioCuentasPagar > 0)
                {
                    //La respuesta es en dia por lo tanto lo dividimo entre los 360 dias que tiene un año fiscal
                    double rotacionCuentasPagarCP = (comprasCredito / promedioCuentasPagar);
                    if (rotacionCuentasPagarCP > 0)
                    {
                        double rotacionCuentasPagarCPDias = (360 / rotacionCuentasPagarCP);
                        txtRotacionPagarCP.Text = Math.Round(rotacionCuentasPagarCPDias).ToString("0.00");
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
            string queryId = "SELECT MAX(E.EndeudamientoID) as EndeudamientoID FROM Endeudamiento as E INNER JOIN Report as RR on E.EndeudamientoID = RR.RentabilidadID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double pasivoTotal = Convert.ToDouble(PT);
                double activoTotal = Convert.ToDouble(AT);
                if (activoTotal > 0)
                {
                    double razonEndeudamientoPorcentual = (pasivoTotal / activoTotal) * (100);
                    txtRazonE.Text = Math.Round(razonEndeudamientoPorcentual,2).ToString("0.00");
                }
                else { MessageBox.Show("Sus Activos Totales deben ser mayores a cero para continuar"); }
            }
            cmd.Close();
        }

        public void PasivoCapital()
        {
            cmd.Open();
            string queryId = "SELECT MAX(E.EndeudamientoID) as EndeudamientoID FROM Endeudamiento as E INNER JOIN Report as RR on E.EndeudamientoID = RR.RentabilidadID WHERE RR.EndeudamientoID = '" + reportID + "'";
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

                double pasivoLargoPlazo = Convert.ToDouble(PLP);
                double capital = Convert.ToDouble(C);
                if (capital > 0)
                {
                    double razonPasivoCapital = (pasivoLargoPlazo / capital);
                    txtRazonPC.Text = Math.Round(razonPasivoCapital,2).ToString("0.00");
                }
                else { MessageBox.Show("Su Capital debe ser mayor a cero para continuar"); }
            }
            cmd.Close();
        }


        #endregion

        #region Razones de Rentabilidad
        public void MBU()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R INNER JOIN Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.RentabilidadID = '" + reportID + "'";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_MBU '" + repID + "'", cmd);
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
                    txtMBU.Text = Math.Round(MBU,2).ToString();
                }
                else { MessageBox.Show("Debe tener Ventas mayor a cero para continuar"); }
                cmd.Close();
            }
            
        }

        public void MUP()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R INNER JOIN Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.RentabilidadID = '" + reportID + "'";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_MUP '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string U = record["Utilidad"].ToString();
                string V = record["Ventas"].ToString();

                double utilidad = Convert.ToDouble(U);
                double ventas = Convert.ToDouble(V);
                if (ventas > 0)
                {
                    double MPU = (utilidad / ventas);
                    txtMUP.Text = Math.Round(MPU,2).ToString();
                }
                else { MessageBox.Show("Debe tener Ventas mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        public void RotacionActivoALP()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R INNER JOIN Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.RentabilidadID = '" + reportID + "'";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_RotacionActivosALP '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string V = record["Ventas"].ToString();
                string TA = record["TotalActivos"].ToString();

                double ventas = Convert.ToDouble(V);
                double totalActivos = Convert.ToDouble(TA);
                if (totalActivos > 0)
                {
                    double rotacionActivo = (ventas / totalActivos);
                    if (rotacionActivo > 0)
                    {
                        double rotacionActivoTotal = (360 / rotacionActivo);
                        txtRotacionActivoALP.Text = Math.Round(rotacionActivoTotal).ToString();
                    }
                    else { MessageBox.Show("Debe tener una rotacion de Activos total mayor a cero para continuar"); }
                }
                else { MessageBox.Show("Debe tener un Total de Activo mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        public void ROA()
        {
            cmd.Open();
            string queryId = "SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R INNER JOIN Report as RR on R.RentabilidadID = RR.RentabilidadID WHERE RR.RentabilidadID = '" + reportID + "'";
            SqlCommand command = new SqlCommand(queryId, cmd);

            int repID = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            SqlCommand query = new SqlCommand("EXEC SP_ROA '" + repID + "'", cmd);
            cmd.Open();

            SqlDataReader record = query.ExecuteReader();
            if (record.Read())
            {
                string U = record["UtilidadNeta"].ToString();
                string TA = record["TotalActivos"].ToString();

                double utilidadNeta = Convert.ToDouble(U);
                double totalActivos = Convert.ToDouble(TA);

                if (totalActivos > 0)
                {
                    double ROA = (utilidadNeta / totalActivos) * 100;
                    //Resultado redondeado a 2 decimale por motivos de porcentaje
                    txtRoa.Text = Math.Round(ROA,2).ToString();
                }
                else { MessageBox.Show("Debe tener Activos totales mayor a cero para continuar"); }
            }
            cmd.Close();
        }

        #endregion
    }
}
