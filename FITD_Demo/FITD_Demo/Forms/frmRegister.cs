using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using ComponentFactory.Krypton.Toolkit;

namespace FITD_Demo.Forms
{
    public partial class frmRegister : KryptonForm
    {
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8\\PAVILION = FITD; Integrated Security = true");
        public frmRegister()
        {
            InitializeComponent();
            LoadComboBox();
        }

        private void btnStarted_Click(object sender, EventArgs e)
        {
            if (!txtNombre.Text.Contains("Ingrese el Nombre de su Proyecto"))
            {
                SuppplierID();
            }
            else
            {
                LoadSelected();
            }
        }

        private void SuppplierID()
        {
            cmd.Open();
            string queryId = "SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L";
            SqlCommand command = new SqlCommand(queryId, cmd);
            int liquidez = Convert.ToInt32(command.ExecuteScalar());
            cmd.Close();

            cmd.Open();
            string queryId2 = "SELECT MAX(E.EndeudamientoID) as EndeudamientoID FROM Endeudamiento AS E";
            SqlCommand command2 = new SqlCommand(queryId2, cmd);
            int endeudamiento = Convert.ToInt32(command2.ExecuteScalar());
            cmd.Close();

            cmd.Open();
            string queryId3 = "SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R";
            SqlCommand command3 = new SqlCommand(queryId3, cmd);
            int rentabilidad = Convert.ToInt32(command3.ExecuteScalar());
            cmd.Close();

            cmd.Open();
            string ReportName = txtNombre.Text;
            SqlCommand query = new SqlCommand("INSERT INTO Report VALUES ('" + ReportName + "','" + liquidez + "','" + endeudamiento + "','" + rentabilidad + "')", cmd);
            int flag = query.ExecuteNonQuery();
            if (flag > 0)
            {
                ntfSaved.ShowBalloonTip(0);
            }
            else { MessageBox.Show("Ups!!, ha ocurrido un error al crear un nuevo Proyecto"); }
            cmd.Close();

            int reportID = liquidez;
            ViewMain viewM = new ViewMain(reportID);
            viewM.Show();
            this.Close();
        }

        public void LoadComboBox()
        {
            SqlCommand loadC = new SqlCommand("SELECT R.Nombre FROM Report as R", cmd);
            cmd.Open();

            SqlDataReader register = loadC.ExecuteReader();
            while (register.Read())
            {
                cmbProyects.Items.Add(register["Nombre"].ToString());
            }

            cmd.Close();
        }

        public void LoadSelected()
        {
            string nombre = cmbProyects.GetItemText(cmbProyects.SelectedItem);
            SqlCommand loadSelected = new SqlCommand("SELECT R.ReportID FROM Report as R where R.Nombre = '" + nombre + "'", cmd);
            cmd.Open();

            SqlDataReader reader = loadSelected.ExecuteReader();
            while (reader.Read())
            {
                int reportID = int.Parse(reader["ReportID"].ToString());

                ViewMain viewM = new ViewMain(reportID);
                viewM.Show();
                this.Close();
            }

            cmd.Close();
        }
    }
}
