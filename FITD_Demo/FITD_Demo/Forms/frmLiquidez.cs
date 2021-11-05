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
    public partial class frmLiquidez : Form
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION; Initial Catalog = FITD; Integrated Security = true");
        
        public frmLiquidez()
        {
            InitializeComponent();
        }

        private void btnStarted_Click(object sender, EventArgs e)
        {
            cmd.Open();
            int activoC = int.Parse(txtActivoC.Text);
            int pasivoC = int.Parse(txtPasivoC.Text);
            int inventario = int.Parse(txtInventario.Text);
            int inventarioI = int.Parse(txtInventarioI.Text);
            int inventarioF = int.Parse(txtInventarioF.Text);
            int costosMV = int.Parse(txtCMVendidas.Text);
            int ventasC = int.Parse(txtVentasCredito.Text);
            int cuentasCobrarI = int.Parse(txtCuentasCobrarI.Text);
            int cuentasCobrarF = int.Parse(txtCuentasCobrarF.Text);
            int cuentasPagarI = int.Parse(txtCuentasPagarI.Text);
            int cuentasPagarF = int.Parse(txtCuentasPagarF.Text);
            int comprasCredito = int.Parse(txtComprasCredito.Text);

            SqlCommand query = new SqlCommand("INSERT INTO Liquidez VALUES('"+activoC+"','" + pasivoC + "','" + inventario + "','" + inventarioI + "','" + inventarioF + "','" + costosMV + "','" + ventasC + "','" + cuentasCobrarI + "','" + cuentasCobrarF + "','" + cuentasPagarI + "','" + cuentasPagarF + "','" + comprasCredito + "')", cmd);
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
            txtActivoC.Text = "";
            txtCMVendidas.Text = "";
            txtComprasCredito.Text = "";
            txtCuentasCobrarF.Text = "";
            txtCuentasCobrarI.Text = "";
            txtCuentasPagarF.Text = "";
            txtCuentasPagarI.Text = "";
            txtInventarioF.Text = "";
            txtInventarioI.Text = "";
            txtPasivoC.Text = "";
            txtVentasCredito.Text = "";
            txtInventario.Text = "";
        }
    }
}
