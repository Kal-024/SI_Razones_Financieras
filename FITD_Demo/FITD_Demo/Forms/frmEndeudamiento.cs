using ComponentFactory.Krypton.Toolkit;
using FITD_Demo.Method;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FITD_Demo.Forms
{
    public partial class frmEndeudamiento : Form
    {
        private ValidateTextBox validator = new ValidateTextBox();


        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION = FITD; Integrated Security = true");
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

        private void txtActivoT_KeyPress(object sender, KeyPressEventArgs e)
        {validator.ValidateForKeyPressed(sender, e);}

        private void txtActivoT_TextChanged(object sender, EventArgs e)
        {validator.ValidateForTextChanged(sender, e);}

        private void txtPasivoLP_KeyPress(object sender, KeyPressEventArgs e)
        {validator.ValidateForKeyPressed(sender,e);}

        private void txtPasivoLP_TextChanged(object sender, EventArgs e)
        {validator.ValidateForTextChanged(sender, e);}

        private void txtPasivoT_KeyPress(object sender, KeyPressEventArgs e)
        {validator.ValidateForKeyPressed(sender, e); }

        private void txtPasivoT_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }

        private void txtCapital_KeyPress(object sender, KeyPressEventArgs e)
        { validator.ValidateForKeyPressed(sender, e); }

        private void txtCapital_TextChanged(object sender, EventArgs e)
        { validator.ValidateForTextChanged(sender, e); }

    }
}
