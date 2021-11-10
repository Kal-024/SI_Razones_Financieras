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
    public partial class frmEstadoR : KryptonForm
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION = FITD; Integrated Security = true");
        public frmEstadoR()
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
        private void cmbProyects_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelected();
        }

        public void LoadSelected()
        {
            int rentabilidadID = 0;
            string nombre = cmbProyects.GetItemText(cmbProyects.SelectedItem);
            SqlCommand loadSelected = new SqlCommand("SELECT R.ReportID,R.RentabilidadID FROM Report as R where R.Nombre = '" + nombre + "'", cmd);
            cmd.Open();

            SqlDataReader reader = loadSelected.ExecuteReader();
            while (reader.Read())
            {
                int reportID = int.Parse(reader["ReportID"].ToString());
                rentabilidadID = int.Parse(reader["LiquidezID"].ToString());
            }
            cmd.Close();

            SqlCommand query = new SqlCommand("SELECT R.Ventas, R.Costos FROM Rentabilidad AS R WHERE R.RentabilidadID = '" + rentabilidadID + "'");
            cmd.Open();

            SqlDataReader readerRentabilidad = loadSelected.ExecuteReader();
            while (readerRentabilidad.Read())
            {
                //Ventas
                lblVentas.Text = "$     ";
                lblVentas.Text += double.Parse(readerRentabilidad["Ventas"].ToString());
                lblVentasNetas.Text = "$     ";
                lblVentasNetas.Text += double.Parse(readerRentabilidad["Ventas"].ToString());
                //Costos
                lblCostoVentas.Text = "$     ";
                lblCostoVentas.Text += double.Parse(readerRentabilidad["Costos"].ToString());
                lblTotalCostoVentas.Text = "$     ";
                lblTotalCostoVentas.Text += double.Parse(readerRentabilidad["Costos"].ToString());
                //Utilidad Bruta
                double utilidadBruta = (double.Parse(readerRentabilidad["Ventas"].ToString()) - double.Parse(readerRentabilidad["Costos"].ToString()));
                lblUtilidadBruta.Text = "$     ";
                lblUtilidadBruta.Text += utilidadBruta.ToString();
                //Utilidad Operacional
                lblUtilidadOperativa.Text = "$     ";
                lblUtilidadOperativa.Text += utilidadBruta.ToString();
                //Utilidad Antes De Impuestos
                lblUtilidadAI.Text = "$     ";
                lblUtilidadAI.Text += utilidadBruta.ToString();
                //Impuestos
                double utilidadAI = (utilidadBruta * 0.3);
                lblImpuestos.Text = "$     ";
                lblImpuestos.Text += utilidadAI.ToString();
                //Utilidad Neta Despues de Impuestos
                double utilidadDespuesdeImpuestos = (utilidadBruta - utilidadAI);
                lblUtilidadNetaDI.Text = "$     ";
                lblUtilidadNetaDI.Text += utilidadDespuesdeImpuestos.ToString();
            }

            cmd.Close();
        }
    }
}
