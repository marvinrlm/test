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
    public class EmpleadosController
    {
        InvercasaDb oConn = new InvercasaDb();

        public List<Empleados> CargarEmpleadosFromTable()
        {
            List<Empleados> empleados = new List<Empleados>();
       

            //string query = "select * from Empleados";

            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spSelEmpleados", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                /*SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spSelEmpleados";
                cmd.CommandType = CommandType.StoredProcedure;*/



                try
                {
                    connection.Open();
                   // var reader = cmd.ExecuteReader();
                   SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        
                        
                        empleados.Add(new Empleados()
                        {
                            Id = reader.GetInt32(0),
                            NombreCompleto = reader.GetString(1),
                            IdTipoIdentificacion = reader.GetInt32(2),
                            NumeroIdentificacion = reader.GetString(3),
                            FechaIngreso = reader.GetDateTime(4),
                            SalarioBaseMensual = reader.GetDecimal(5),
                            Direccion = reader.GetString(6),
                        });


                       /* Empleados oEmpleado = new Empleados();
                        oEmpleado.Id = reader.GetInt32(0);
                        oEmpleado.NombreCompleto = reader.GetString(1);
                        oEmpleado.IdTipoIdentificacion = reader.GetInt32(2);
                        oEmpleado.NumeroIdentificacion = reader.GetString(3);
                        oEmpleado.FechaIngreso = reader.GetDateTime(4);
                        oEmpleado.SalarioBaseMensual = reader.GetDecimal(5);
                        oEmpleado.Direccion = reader.GetString(6);
                        empleados.Add(oEmpleado);*/


                    }
                    reader.Close();
                    connection.Close(); 
                }catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: "+ ex.Message);
                    
                }
            }
            return empleados;
        }

        public Empleados CargarEmpleadoFromId(int id )
        {
          Empleados empleados = new Empleados();


            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spSelEmpleadoId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    // var reader = cmd.ExecuteReader();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    


                        empleados.Id = reader.GetInt32(0);
                        empleados.NombreCompleto = reader.GetString(1);
                        empleados.IdTipoIdentificacion = reader.GetInt32(2);
                        empleados.NumeroIdentificacion = reader.GetString(3);
                        empleados.FechaIngreso = reader.GetDateTime(4);
                        empleados.SalarioBaseMensual = reader.GetDecimal(5);
                        empleados.Direccion = reader.GetString(6);
            

                    
                    reader.Close();
                    connection.Close();
                    return empleados;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }
            }

        }

        public DataTable CargarEmpleadosFromView()
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                   // connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelvEmpleados", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //connection.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }
        }

        public void Add(string NombreCompleto, int IdTipoIdentificacion
                       , string NumeroIdentificacion
                       , DateTime FechaIngreso
                       , decimal SalarioBaseMensual
                       , string Direccion)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spInsEmpleado", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                var paramId = new SqlParameter("@Id", SqlDbType.Int);
                paramId.Direction = ParameterDirection.Output;


                cmd.Parameters.Add(paramId);
                cmd.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", IdTipoIdentificacion);
                cmd.Parameters.AddWithValue("@NumeroIdentificacion", NumeroIdentificacion);
                cmd.Parameters.AddWithValue("@FechaIngreso", FechaIngreso);
                cmd.Parameters.AddWithValue("@SalarioBaseMensual", SalarioBaseMensual);
                cmd.Parameters.AddWithValue("@Direccion", Direccion);

                connection.Open();
                cmd.ExecuteNonQuery();
                var id = (int)paramId.Value;
                MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }




        public void Edit(int Id, string NombreCompleto, int IdTipoIdentificacion
                           , string NumeroIdentificacion
                           , DateTime FechaIngreso
                           , decimal SalarioBaseMensual
                           , string Direccion)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spUpdEmpleado", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", IdTipoIdentificacion);
                cmd.Parameters.AddWithValue("@NumeroIdentificacion", NumeroIdentificacion);
                cmd.Parameters.AddWithValue("@FechaIngreso", FechaIngreso);
                cmd.Parameters.AddWithValue("@SalarioBaseMensual", SalarioBaseMensual);
                cmd.Parameters.AddWithValue("@Direccion", Direccion);

                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }



        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spDelEmpleado", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                cmd.Parameters.AddWithValue("@Id", Id);


                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }








    }
}
