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
    public partial class frmClientAU : Form
    {
        private ClientRepository clientRepository;
        public bool update;
        public Cliente clientToUpdate { get; set; }
        public frmClientAU()
        {
            InitializeComponent();
            clientRepository = new ClientRepository();
        }

        public void fillSpaces(Cliente c)
        {
            txtName.Text = c.Name;
            txtLastName.Text = c.LastName;
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastName) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Uno de los campos esta vacío!!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                if (update == true)
            {
                clientToUpdate.Name = name;
                clientToUpdate.LastName = lastName;
                clientToUpdate.Email = email;
                clientToUpdate.Phone = phone;
                clientRepository.Update(clientToUpdate);
                this.Dispose();
                return;
            }

            Cliente c = new Cliente
            {
                Name = name,
                LastName = lastName,
                Email = email,
                Phone = phone
            };

            clientRepository.Create(c);
            this.Dispose();
        }

        private void validateTextFileds(KeyPressEventArgs e, int validate)
        {
            if(validate == 0)
            {
                if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
                {
                    MessageBox.Show("Solo letras", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
            else if(validate == 1)
            {
                if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
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

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 1);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            validateTextFileds(e, 2);
        }
    }
}
