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

namespace FITD_Demo.Forms
{
    public partial class frmRentabilidad : Form
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION; Initial Catalog = FITD; Integrated Security = true");
        public frmRentabilidad()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Open();

            int ventas = int.Parse(txtVentas.Text);
            int costos = int.Parse(txtCostos.Text);
            int totalActivos = int.Parse((txtTotalActivos.Text));
            int utilidadNeta = int.Parse(txtUtilidadNeta.Text);

            SqlCommand query = new SqlCommand("INSERT INTO Rentabilidad VALUES('" + ventas + "','" + costos + "','" + totalActivos + "','" + utilidadNeta + "')", cmd);
            int flag = query.ExecuteNonQuery();

            if (flag > 0)
            {
                ntfSaved.ShowBalloonTip(0);
            }
            else
            {
                MessageBox.Show("Ups!, No se pudo guardar los datos con la BD FITD");
            }

            cmd.Close();
            Cleanner();
        }

        private void Cleanner()
        {
            txtVentas.Text = "";
            txtCostos.Text = "";
            txtTotalActivos.Text = "";
            txtUtilidadNeta.Text = "";
        }
    }
}
