using FITD_Demo.Method;
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
        ValidateTextBox validator = new ValidateTextBox();

        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-N7H71KN; Initial Catalog = FITD; Integrated Security = true");
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

        private void txtVentas_KeyPress(object sender, KeyPressEventArgs e)
        { validator.ValidateForKeyPressed(sender, e);}

        private void txtVentas_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }

        private void txtCostos_KeyPress(object sender, KeyPressEventArgs e)
        { validator.ValidateForKeyPressed(sender, e); }

        private void txtCostos_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }

        private void txtTotalActivos_KeyPress(object sender, KeyPressEventArgs e)
        { validator.ValidateForKeyPressed(sender, e); }

        private void txtTotalActivos_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }

        private void txtUtilidadNeta_KeyPress(object sender, KeyPressEventArgs e)
        { validator.ValidateForKeyPressed(sender, e); }

        private void txtUtilidadNeta_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }
    }
}
