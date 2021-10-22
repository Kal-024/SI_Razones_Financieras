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

            SqlCommand query2 = new SqlCommand("SELECT MAX(L.LiquidezID) as LiquidezID, MAX(E.EndeudamientoID) as EndeudamientoID, MAX(R.RentabilidadID) as RentabilidadID  from Report as RR inner join Liquidez as L on RR.ReportID = L.LiquidezID inner join Endeudamiento as E on RR.ReportID = E.EndeudamientoID inner join Rentabilidad as R on RR.ReportID = R.RentabilidadID", cmd);
            cmd.Open();
            
            SqlDataReader record = query2.ExecuteReader();
            int liquidez = int.Parse(record["LiquidezID"].ToString());
            int endeudamiento = int.Parse(record["EndeudamientoID"].ToString());
            int rentabilidad = int.Parse(record["RentabilidadID"].ToString());


            //int liquidez = int.Parse(l);
            //int endeudamiento = int.Parse(e);
            //int rentabilidad = int.Parse(r);
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

            Cleanner();
        }

        private void Cleanner()
        {
            txtNombre.Text = "";
        }
        
    }
}
