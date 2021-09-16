using Core.Poco;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class frmClients : Form
    {
        private ClientRepository clientRepository;
        public frmClients()
        {
            InitializeComponent();
            txtSearch.AutoSize = false;
            this.MinimumSize = new Size(651, 298);
            clientRepository = new ClientRepository();
            dgvClients.DataSource = clientRepository.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmClientAU frmClientAU = new frmClientAU();
            frmClientAU.ShowDialog();
            frmClientAU.update = false;
            dgvClients.DataSource = clientRepository.GetAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvClients.Rows.Count == 0 || dgvClients.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Esta tabla esta vacia, o intente seleccionar otra fila pls");
                return;
            }
            Cliente c = (Cliente)dgvClients.CurrentRow.DataBoundItem;
            if (c == null)
            {
                return;
            }
            frmClientAU frmClientAU = new frmClientAU();
            frmClientAU.fillSpaces(c);
            frmClientAU.update = true;
            frmClientAU.clientToUpdate = c;
            frmClientAU.ShowDialog();
            dgvClients.Refresh();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.Rows.Count == 0 || dgvClients.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Esta tabla esta vacia, o intente seleccionar otra fila pls");
                return;
            }
            Cliente c = (Cliente)dgvClients.CurrentRow.DataBoundItem;
            Console.WriteLine(c.Id);
            clientRepository.Delete(c);
            dgvClients.DataSource = clientRepository.GetAll();

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvClients.Rows.Count == 0)
            {
                return;

            }

            List<Cliente> filtro = new List<Cliente>();
            string Clave = txtSearch.Text.ToUpper();
            foreach (Cliente cli in clientRepository.GetAll())
            {
                if ((cli.Id + "").ToUpper().Contains(Clave) || cli.Name.ToUpper().Contains(Clave) ||
                    (cli.LastName + "").ToUpper().Contains(Clave)||
                    cli.Email.ToUpper().Contains(Clave) || cli.Phone.ToUpper().Contains(Clave)) 
                    filtro.Add(cli);
            }

            if (filtro.Count > 0)
                dgvClients.DataSource = filtro;
            else
            {
                dgvClients.DataSource = clientRepository.GetAll();
            }

        }
    }
    }

