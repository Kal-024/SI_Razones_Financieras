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
    public partial class frmClientes : Form
    {
        private ClientRepository clientRepository;
        public frmClientes()
        {
            InitializeComponent();
            txtSearch.AutoSize = false;
            this.MinimumSize = new Size(651, 298);
            clientRepository = new ClientRepository();
            //AGREGAR EL SETDATAResource
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
