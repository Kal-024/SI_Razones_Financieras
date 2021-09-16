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
    public partial class Form1 : Form
    {
        
        public Form1()
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
    }
}
