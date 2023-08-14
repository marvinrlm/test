using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    public class InvercasaDb
    {
        private readonly string _ConnectionString = "Data Source=.\\SQLExpress;Initial Catalog=invercasa;User=sa;Password=123456;Encrypt=False;";

        private string DefaultConnectionString;

        public InvercasaDb() {
            DefaultConnectionString =_ConnectionString;
        }
        public string Conn()
        {
            return DefaultConnectionString;
        }
        public bool Ok()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Conn());
                connection.Open();
            }
            catch { 
                return false;
            }
            return true;

        }



    }




}
