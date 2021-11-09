using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FITD_Demo.Forms
{
    public partial class frmCobertura : Form
    {
        private Form activeForm = null;
        public frmCobertura()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //OpenChildForm(new Forms.frmEndeudamiento());
        }

        //private void OpenChildForm(Form childForm)
        //{
        //    if (activeForm != null)
        //    {
        //        activeForm.Close();
        //    }
        //    activeForm = childForm;
        //    childForm.TopLevel = false;
        //    childForm.FormBorderStyle = FormBorderStyle.None;
        //    childForm.Dock = DockStyle.Fill;
        //    pnlChildForm.Controls.Add(childForm);
        //    pnlChildForm.Tag = childForm;
        //    childForm.BringToFront();
        //    childForm.Show();
        //}

    }
}
