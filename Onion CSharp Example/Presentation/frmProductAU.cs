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
    public partial class frmProductAU : Form
    {
        private ProductRepository productRepository;
        public bool update;
        private int onePoint = 0;
        public Product productToUpdate { get; set; }
        public frmProductAU()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            //string rutaImagen = string.Empty;
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Elige la ruta de la imagen";
            //ofd.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png|GIF(*.gif)|*.gif";

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    rutaImagen = ofd.FileName;
            //}

            //txtImage.Text = rutaImagen;
            using (OpenFileDialog ofd = new OpenFileDialog() {Title = "Elige la ruta de la imagen", Multiselect = false, ValidateNames = true, Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"})
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImage.Text = ofd.FileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace(txtDescription.Text) || String.IsNullOrWhiteSpace(txtBrand.Text) ||
                String.IsNullOrWhiteSpace(txtModel.Text) || String.IsNullOrWhiteSpace(txtPrice.Text) || String.IsNullOrWhiteSpace(txtStock.Text) ||
                String.IsNullOrWhiteSpace(txtImage.Text))
            {
                MessageBox.Show("Uno de los campos esta vacío!!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            string description = txtDescription.Text;
            string brand = txtBrand.Text;
            string model = txtModel.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int stock = int.Parse(txtStock.Text);
            string image = txtImage.Text;

            if (update == true)
            {
                productToUpdate.Name = name;
                productToUpdate.Description = description;
                productToUpdate.Brand = brand;
                productToUpdate.Model = model;
                productToUpdate.Price = price;
                productToUpdate.Stock = stock;
                productToUpdate.ImageURL = image;
                productRepository.Update(productToUpdate);
                this.Dispose();
                return;
            }

            Product p = new Product
            {
                Name = name,
                Description = description,
                Brand = brand,
                Model = model,
                Price = price,
                Stock = stock,
                ImageURL = image,
            };

            productRepository.Create(p);

            this.Dispose();
        }

        public void fillSpaces(Product p)
        {
            txtName.Text = p.Name;
            txtDescription.Text = p.Description;
            txtBrand.Text = p.Brand;
            txtModel.Text = p.Model;
            txtPrice.Text = p.Price.ToString();
            txtStock.Text = p.Stock.ToString();
            txtImage.Text = p.ImageURL;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void validateTextFileds(KeyPressEventArgs e, int validate)
        {
            if (validate == 0)
            {
                if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
                {
                    MessageBox.Show("Solo letras", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
            else if (validate == 1)
            {
                if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar == 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                {
                    MessageBox.Show("Solo números", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar == 47) || (e.KeyChar >= 58 && e.KeyChar <= 63) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
                {
                    MessageBox.Show("Solo números, letras, arroba ó punto", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 0);
        }

        private void txtBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 0);
        }

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 2);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 1);
            if (e.KeyChar == 46)
            {
                onePoint++;
                if (onePoint == 0 || onePoint > 1)
                {
                    MessageBox.Show("No mas de un punto '.'", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 1);
        }
    }
}
