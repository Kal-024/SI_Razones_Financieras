using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class frmBalanceG : KryptonForm
    {

        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION; Initial Catalog = FITD; Integrated Security = true");

        public frmBalanceG()
        {
            InitializeComponent();
            LoadComboBox();
        }

        public void LoadComboBox()
        {
            SqlCommand loadC = new SqlCommand("SELECT R.Nombre FROM Report as R", cmd);
            cmd.Open();

            SqlDataReader register = loadC.ExecuteReader();
            while (register.Read())
            {
                cmbProyects.Items.Add(register["Nombre"].ToString());
            }

            cmd.Close();
        }



        public void LoadSelected()
        {
            int liquizId = 0;
            int endeudamId = 0;
            string nombre = cmbProyects.GetItemText(cmbProyects.SelectedItem);
            SqlCommand loadSelected = new SqlCommand("SELECT R.ReportID,R.LiquidezID,R.EndeudamientoID FROM Report as R where R.Nombre = '" + nombre + "'", cmd);
            cmd.Open();

            SqlDataReader reader = loadSelected.ExecuteReader();
            while (reader.Read())
            {
                int reportID = int.Parse(reader["ReportID"].ToString());
                liquizId = int.Parse(reader["LiquidezID"].ToString());
                endeudamId = int.Parse(reader["EndeudamientoID"].ToString());
            }

            cmd.Close();

            SqlCommand loadLiquizId = new SqlCommand("Select Inventario,abs(CuentasPorCobrarFinal-CuentasPorCobrarInicial) as [Cuentas por Cobrar],abs(CuentasPorPagarFinal-CuentasPorPagarInicial) as [Cuentas por Pagar] from Liquidez where LiquidezID = '" + liquizId + "'", cmd);
            cmd.Open();
            SqlDataReader readerLiquiz = loadLiquizId.ExecuteReader();
            while (readerLiquiz.Read())
            {
                lblCuentasC.Text = "$     ";
                lblCuentasC.Text += decimal.Parse(readerLiquiz["Cuentas por Cobrar"].ToString());
                lblInventario.Text = "$     ";
                lblInventario.Text += decimal.Parse(readerLiquiz["Inventario"].ToString());
                lblTotalActivoC.Text = "$     ";
                decimal sumaActivo = ((decimal)readerLiquiz["Cuentas por Cobrar"] + (decimal)readerLiquiz["Inventario"]);
                lblTotalActivoC.Text += decimal.Parse(sumaActivo.ToString());
                lblSumaActivo.Text = lblTotalActivoC.Text; 
                lblCuentasP.Text = "$     ";
                lblCuentasP.Text += decimal.Parse(readerLiquiz["Cuentas por Pagar"].ToString());
                lblTotalPasivoC.Text = lblCuentasP.Text;
            }
            cmd.Close();

            SqlCommand loadRentabilidad = new SqlCommand("Select PasivoLargoPlazo,Capital From Endeudamiento where EndeudamientoID = '" + endeudamId + "'", cmd);
            cmd.Open();
            SqlDataReader readerEndeuda = loadRentabilidad.ExecuteReader();
            while (readerEndeuda.Read())
            {
                lblTotalPasivoLp.Text = "$     ";
                lblTotalPasivoLp.Text += decimal.Parse(readerEndeuda["PasivoLargoPlazo"].ToString());
                lblSumaCC.Text = "$     ";
                lblSumaCC.Text += decimal.Parse(readerEndeuda["Capital"].ToString());
            }
            cmd.Close();
            lblSumaPasivo.Text = "$     ";
            decimal totalPasivo = Convert.ToDecimal(lblTotalPasivoC.Text.Substring(6)) + Convert.ToDecimal(lblTotalPasivoLp.Text.Substring(6));
            lblSumaPasivo.Text += totalPasivo.ToString();
            

            lblSumaPC.Text = "$     ";
            decimal sumaPC = Convert.ToDecimal(lblSumaCC.Text.Substring(6)) + Convert.ToDecimal(lblSumaPasivo.Text.Substring(6));
            lblSumaPC.Text += sumaPC.ToString();
        }

        private void cmbProyects_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelected();
        }
    }
}
