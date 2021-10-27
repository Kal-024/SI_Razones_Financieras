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
        SqlConnection cmd = new SqlConnection("Data Source = DESKTOP-JBS2MU8; Initial Catalog = FITD; Integrated Security = true");
        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnStarted_Click(object sender, EventArgs e)
        {
            SuppplierID();

            //Abriendo el Form Main
            ViewMain viewM = new ViewMain();
            viewM.Show();
            this.Close();
        }

        private void SuppplierID()
        {
            SqlCommand query3 = new SqlCommand("SELECT MAX(E.EndeudamientoID) as EndeudamientoID FROM Endeudamiento AS E", cmd);
            cmd.Open();
            SqlDataReader record3 = query3.ExecuteReader();
            int endeudamiento = int.Parse(record3["EndeudamientoID"].ToString());
            cmd.Close();

            SqlCommand query2 = new SqlCommand("SELECT MAX(L.LiquidezID) as LiquidezID FROM Liquidez as L", cmd);
            cmd.Open();
            SqlDataReader record = query2.ExecuteReader();
            int liquidez = int.Parse(record["LiquidezID"].ToString());
            cmd.Close();
            
            SqlCommand query4 = new SqlCommand("SELECT MAX(R.RentabilidadID) as RentabilidadID FROM Rentabilidad AS R", cmd);
            cmd.Open();
            SqlDataReader record4 = query4.ExecuteReader();
            int rentabilidad = int.Parse(record4["RentabilidadID"].ToString());
            cmd.Close();
            


            //int liquidez = int.Parse(l);
            //int endeudamiento = int.Parse(e);
            //int rentabilidad = int.Parse(r);
            
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

            Cleanner();
        }

        private void Cleanner()
        {
            txtNombre.Text = "";
        }
        
    }
}
