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
    public partial class frmProductos : Form
    {
        private ProductRepository productRepository;
        public frmProductos()   
        {
            InitializeComponent();
            txtSearch.AutoSize = false;
            this.MinimumSize = new Size(651, 298);
            productRepository = new ProductRepository();
            dgvProducts.DataSource = productRepository.GetAll();
        }

   
        private void frmProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductAU frmProductAU = new frmProductAU();
            frmProductAU.ShowDialog();
            frmProductAU.update = false;
            dgvProducts.DataSource = productRepository.GetAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.Rows.Count == 0 || dgvProducts.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Tabla sin datos o fila no seleccionada");
                return;
            }
            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            if(p == null)
            {
                return;
            }
            frmProductAU frmProductAU = new frmProductAU();
            frmProductAU.fillSpaces(p);
            frmProductAU.update = true;
            frmProductAU.productToUpdate = p;
            frmProductAU.ShowDialog();
            dgvProducts.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvProducts.Rows.Count == 0 || dgvProducts.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Tabla sin datos o fila no seleccionada");
                return;
            }

            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            Console.WriteLine(p.Id);
            productRepository.Delete(p);
            dgvProducts.DataSource = productRepository.GetAll();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

            if (dgvProducts.Rows.Count == 0)
            {
                return;

            }

            List<Product> filtro = new List<Product>();
            string Clave = txtSearch.Text.ToUpper();
            foreach (Product pro in productRepository.GetAll())
            {
                if ((pro.Id + "").ToUpper().Contains(Clave) || pro.Name.ToUpper().Contains(Clave) || (pro.Description + "").ToUpper().Contains(Clave)
                    || pro.Brand.ToUpper().Contains(Clave) || pro.Model.ToUpper().Contains(Clave) || (pro.Price + "").ToUpper().Contains(Clave)
                    || (pro.Stock + "").ToUpper().Contains(Clave) || pro.ImageURL.ToUpper().Contains(Clave))
                    filtro.Add(pro);

            }

            if (filtro.Count > 0)
                dgvProducts.DataSource = filtro;
            else
            {
                dgvProducts.DataSource = productRepository.GetAll();
            }
               
        }
    }
}
