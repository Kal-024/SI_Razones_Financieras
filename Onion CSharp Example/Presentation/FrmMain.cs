using Core.Poco;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class FrmMain : Form
    {
        
        public FrmMain()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductos frmProductos = new frmProductos();
            frmProductos.MdiParent = this;
            frmProductos.Show();
        }

        private void productosViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClients frmClients = new frmClients();
            frmClients.MdiParent = this;
            frmClients.Show();
        }

        private void btnIngresoP_Click(object sender, EventArgs e)
        {
            
            FrmIngresoP frmIngresoP = new FrmIngresoP();
            frmIngresoP.ShowDialog();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            InicioPresentacion inicioPresentacion = new InicioPresentacion();
            PnlMain.Controls.Add(inicioPresentacion);

        }

        private void btnCuentas_Click(object sender, EventArgs e)
        {

            FrmCuentas frmCuenta = new FrmCuentas();
            frmCuenta.ShowDialog();

        }

        private void btnBalanceG_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            UcBalanceG ucBalanceG= new UcBalanceG();
            PnlMain.Controls.Add(ucBalanceG);
        }

        private void btnEstadoR_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            UcEstadoR ucEstadoR = new UcEstadoR();
            PnlMain.Controls.Add(ucEstadoR);
        }

        private void btnAnalisisV_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            UcAnalisisV ucAnalisisV = new UcAnalisisV();
            PnlMain.Controls.Add(ucAnalisisV);
        }

        private void btnAnalisisH_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            UcAnalisisH ucAnalisisH = new UcAnalisisH();
            PnlMain.Controls.Add(ucAnalisisH);
        }

        private void btnRazonesF_Click(object sender, EventArgs e)
        {
            PnlMain.Controls.Clear();
            UcRazonesF ucRazonesF = new UcRazonesF();
            PnlMain.Controls.Add(ucRazonesF);
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            InicioPresentacion inicioPresentacion = new InicioPresentacion();
            inicioPresentacion.Dock = DockStyle.Fill;
            PnlMain.Controls.Add(inicioPresentacion);
        }
    }
}
