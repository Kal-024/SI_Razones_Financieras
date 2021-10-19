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
    public partial class frmEndeudamiento : Form
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8; Initial Catalog = FITD; Integrated Security = true");
        public frmEndeudamiento()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Open();

            int activoT = int.Parse(txtActivoT.Text);
            int pasivoT = int.Parse(txtPasivoT.Text);
            int pasivoLP = int.Parse(txtPasivoLP.Text);
            int capital = int.Parse(txtCapital.Text);

            SqlCommand query = new SqlCommand("INSERT INTO Endeudamiento VALUES('" + activoT + "','" + pasivoT + "','" + pasivoLP + "','" + capital + "')", cmd);
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
            txtActivoT.Text = "";
            txtPasivoT.Text = "";
            txtPasivoLP.Text = "";
            txtCapital.Text = "";
        }
    }
}
