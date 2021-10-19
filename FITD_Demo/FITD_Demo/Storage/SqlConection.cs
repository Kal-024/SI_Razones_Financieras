using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data.SqlClient;
using System.Configuration;

namespace FITD_Demo.Storage
{
    public class SqlConection
    {
        public SqlConnection ReadString()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["unique"].ConnectionString);

            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            else
            {
                cn.Open();
            }

            return cn;
        }
    }
}
