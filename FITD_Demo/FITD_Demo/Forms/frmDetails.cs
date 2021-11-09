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
    public partial class frmDetails : KryptonForm
    {
        SqlConnection cmd = new SqlConnection("Data Source = TV-236; Initial Catalog = FITD; Integrated Security = true");
        public frmDetails()
        {
            InitializeComponent();
        }
    }
}
