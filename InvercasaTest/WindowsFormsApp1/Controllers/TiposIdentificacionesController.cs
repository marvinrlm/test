using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class TiposIdentificacionesController
    {
        InvercasaDb oConn = new InvercasaDb();

        public  DataTable CargarComboBoxTipoIdentificacion()
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelTipoIdentificacion", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    connection.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: "+ ex.Message);
                    
                }
                
            }

        }











    }
}
