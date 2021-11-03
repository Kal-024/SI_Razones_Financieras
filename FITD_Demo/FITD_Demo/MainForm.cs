using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace FITD_Demo
{
    public partial class MainForm : KryptonForm
    {
        public MainForm()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        #region Customize Dynamic Menus
        private void CustomizeDesign()
        {
            pnlSubMenuReport.Visible = false;
        }

        private void HideSubMenu()
        {
            if (pnlSubMenuReport.Visible == true)
            {
                pnlSubMenuReport.Visible = false;
            }
        }
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
#endregion

        #region Add Report
        private void btnAddReport_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlSubMenuReport);
        }

        private void btnLiquidez_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmLiquidez());
            HideSubMenu();

            //Storage.SqlConection conection = new Storage.SqlConection();
            //conection.FITDSQLConnectionOpen();
        }

        private void btnEndeudamiento_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmEndeudamiento());
            //..
            //My Codes
            //..
            HideSubMenu();
        }

        private void btnRentabilidad_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmRentabilidad());
            //..
            //My Codes
            //..
            HideSubMenu();
        }

        private void btnCobertura_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmCobertura());
            //..
            //My Codes
            //..
            HideSubMenu();
        }
        #endregion

        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlChildForm.Controls.Add(childForm);
            pnlChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        #region Open Word Document
        private void btnOprnDoc_Click(object sender, EventArgs e)
        {
            OpenDoc();
        }

        private void OpenDoc()
        {
            string path = @"C:\Users\Usuario\source\repos\FITD_Demo\FITD_Demo\Resources\DATOSDERAZONESDELIQUIDEZPARAPROYECTODECURSO.docx";
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "WINWORD.EXE";
            psi.Arguments = path;
            Process.Start(psi);
        }
#endregion

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Forms.frmRegister frmR = new Forms.frmRegister();
            frmR.Show();           
        }
    }
}
