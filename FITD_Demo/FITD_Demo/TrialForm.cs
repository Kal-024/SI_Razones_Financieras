using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace FITD_Demo
{
    public partial class TrialForm : KryptonForm
    {
        public TrialForm()
        {
            InitializeComponent();
        }

        private void txtEmail_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmail.Text = "";
        }

        private void btnStarted_Click(object sender, EventArgs e)
        {            
            MainForm mainF = new MainForm();
            mainF.ShowDialog();
        }
    }
}
