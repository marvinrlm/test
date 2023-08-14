using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Views;

namespace WindowsFormsApp1.Controllers
{
    public class RegistrosVacacionesController
    {
        InvercasaDb oConn = new InvercasaDb();

        public  DataTable CargarEstadoVacaciones()
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelEstadoVacacion", connection);
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


        public DataTable CargarEmpleado()
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelEmpleados", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    connection.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }

        }


        public DataTable CargarRegistroVacacionesId(int id)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelvRegistroVacacionesId", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", id);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    connection.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }
        }

        public void Add(int IdEmpleado,DateTime FechaVacacion, int IdEstadoVacacion)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spInsRegistroVacacion", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                //cmd.Parameters.Add(paramId);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                cmd.Parameters.AddWithValue("@FechaVacacion", FechaVacacion);
                cmd.Parameters.AddWithValue("@IdEstadoVacacion", IdEstadoVacacion);


                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }



        public void Edit(int IdEmpleado, DateTime FechaVacacion, int IdEstadoVacacion)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spUpdRegistroVacaciones", connection);
                cmd.CommandType = CommandType.StoredProcedure;
               
                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                //cmd.Parameters.Add(paramId);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                cmd.Parameters.AddWithValue("@FechaVacacion", FechaVacacion);
                cmd.Parameters.AddWithValue("@IdEstadoVacacion", IdEstadoVacacion);


                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }



        public string Calcular1(DateTime FechaInicio, DateTime FechaFin)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT dbo.fnCalcularVacaciones(@FechaInicio,@FechaFin)", connection);
                    // cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    string str = dt.Rows[0][0].ToString();
                    
                   // MessageBox.Show((str.ToString()));

                    return str;


                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }
        }


        public DataTable BuscarVacacionesSaldosId(int idEmpleado,int Ano, int Mes)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {
                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spSelVacacionesSaldosId", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    da.SelectCommand.Parameters.AddWithValue("@Ano", Ano);
                    da.SelectCommand.Parameters.AddWithValue("@Mes", Mes);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    connection.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }
        }

        public void AddVacacionesSaldos(int IdEmpleado, int Ano, int Mes)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spInsRegistroVacacion", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                //cmd.Parameters.Add(paramId);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                cmd.Parameters.AddWithValue("@Ano", Ano);
                cmd.Parameters.AddWithValue("@Mes", Mes);


                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }



        public void EditVacacionesSaldos(int IdEmpleado, int Ano, int Mes,int VacacionesTomadas)
        {
            using (SqlConnection connection = new SqlConnection(oConn.Conn()))

            {

                //SqlCommand cmd = new SqlCommand(query, connection);
                SqlCommand cmd = new SqlCommand("spUpdRegistroVacaciones", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                //paramId.Direction = ParameterDirection.Output;


                //cmd.Parameters.Add(paramId);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                cmd.Parameters.AddWithValue("@Ano", Ano);
                cmd.Parameters.AddWithValue("@Mes", Mes);
                cmd.Parameters.AddWithValue("@VacacionesTomadas", VacacionesTomadas);



                connection.Open();
                cmd.ExecuteNonQuery();
                //var id = (int)paramId.Value;
                //MessageBox.Show("ID Retornado" + id);
                connection.Close();
            }
        }




        //AGREGAR VACACIONES CON TRANSACTIONS

        public void RegistroVacacionesTrans(int IdEmpleado, DateTime FechaVacacion, int IdEstadoVacacion, int Ano, int Mes)
        {

             decimal numRegVacSaldos=0;

            //MessageBox.Show("Recibiendo parametros empleado: " + IdEmpleado + " Fecha " + FechaVacacion + " IdEstado " + IdEstadoVacacion + " Ano " + Ano + " Mes " + Mes);

            try
            {
                using (TransactionScope scope1 = new TransactionScope())
                //Default is Required
                {
                    using (TransactionScope scope2 = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (SqlConnection connection = new SqlConnection(oConn.Conn()))

                        {

                            //SqlCommand cmd = new SqlCommand(query, connection);
                            SqlCommand cmd = new SqlCommand("spInsRegistroVacacion", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                            //paramId.Direction = ParameterDirection.Output;


                            //cmd.Parameters.Add(paramId);
                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                            cmd.Parameters.AddWithValue("@FechaVacacion", FechaVacacion);
                            cmd.Parameters.AddWithValue("@IdEstadoVacacion", IdEstadoVacacion);


                            connection.Open();
                            cmd.ExecuteNonQuery();
                            //var id = (int)paramId.Value;
                            //MessageBox.Show("ID Retornado" + id);
                            connection.Close();
                        }
                        scope2.Complete();
                       
                    }

                    using (TransactionScope scope3 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //buscar el registro de vacaciones saldos
                        using (SqlConnection connection = new SqlConnection(oConn.Conn()))

                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter da = new SqlDataAdapter("spSelVacacionesSaldosId", connection);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                da.SelectCommand.Parameters.AddWithValue("@Ano", Ano);
                                da.SelectCommand.Parameters.AddWithValue("@Mes", Mes);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                connection.Close();
                                //return dt;
                                if (IdEstadoVacacion == 2) { 
                                    if (dt.Rows.Count > 0 )
                                    {
                                        //Actualizar
                                        //numRegVacSaldos = (int)dt.Rows[0][4];
                                        var cellValue = dt.Rows[0][4];
                                       // MessageBox.Show("Vacaciones saldos: " + cellValue);
                                        numRegVacSaldos = (decimal)cellValue + 1;
                                    
                                       // MessageBox.Show("Vacaciones saldos: " + numRegVacSaldos);


                                        using (SqlConnection connection1 = new SqlConnection(oConn.Conn()))

                                        {

                                            //SqlCommand cmd = new SqlCommand(query, connection);
                                            SqlCommand cmd = new SqlCommand("spUpdVacacionesSaldos", connection1);
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                                            //paramId.Direction = ParameterDirection.Output;


                                            //cmd.Parameters.Add(paramId);
                                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                            cmd.Parameters.AddWithValue("@Ano", Ano);
                                            cmd.Parameters.AddWithValue("@Mes", Mes);
                                            cmd.Parameters.AddWithValue("@VacacionesTomadas", numRegVacSaldos);



                                            connection1.Open();
                                            cmd.ExecuteNonQuery();
                                            //var id = (int)paramId.Value;
                                            //MessageBox.Show("ID Retornado" + id);
                                            connection1.Close();
                                            scope3.Complete();
                                        }




                                    }
                                    else
                                    {
                                        //insertar

                                        numRegVacSaldos ++;

                                        using (SqlConnection connection2 = new SqlConnection(oConn.Conn()))

                                        {

                                            //SqlCommand cmd = new SqlCommand(query, connection);
                                            SqlCommand cmd = new SqlCommand("spInsVacacionesSaldos", connection2);
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                                            //paramId.Direction = ParameterDirection.Output;

                                            decimal vacAcum = (decimal)2.5;
                                            //cmd.Parameters.Add(paramId);
                                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                            cmd.Parameters.AddWithValue("@Ano", Ano);
                                            cmd.Parameters.AddWithValue("@Mes", Mes);
                                            cmd.Parameters.AddWithValue("@VacacionesAcumuladas", vacAcum);
                                            cmd.Parameters.AddWithValue("@VacacionesTomadas", numRegVacSaldos);



                                            connection2.Open();
                                            cmd.ExecuteNonQuery();
                                            //var id = (int)paramId.Value;
                                            //MessageBox.Show("ID Retornado" + id);
                                            connection2.Close();
                                            scope3.Complete();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Hay un error en la BD: " + ex.Message);

                            }

                        }

                         


                    }

                    /*
                    using (TransactionScope scope4 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //...  
                    }


                    using (TransactionScope scope5 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //...  
                    }


                    using (TransactionScope scope4 = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        //...  
                    }
                    */
                    scope1.Complete();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }


        public void EditarVacacionesTrans(int IdEmpleado, DateTime FechaVacacion, int IdEstadoVacacion, int Ano, int Mes)
        {

            decimal numRegVacSaldos = 0;

           // MessageBox.Show("Recibiendo parametros empleado: " + IdEmpleado + " Fecha " + FechaVacacion + " IdEstado " + IdEstadoVacacion + " Ano " + Ano + " Mes " + Mes);

            
            try
            {
                using (TransactionScope scope1 = new TransactionScope())
                //Default is Required
                {
                    using (TransactionScope scope2 = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (SqlConnection connection = new SqlConnection(oConn.Conn()))

                        {

                            //SqlCommand cmd = new SqlCommand(query, connection);
                            SqlCommand cmd = new SqlCommand("spUpdRegistroVacaciones", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                            //paramId.Direction = ParameterDirection.Output;


                            //cmd.Parameters.Add(paramId);
                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                            cmd.Parameters.AddWithValue("@FechaVacacion", FechaVacacion);
                            cmd.Parameters.AddWithValue("@IdEstadoVacacion", IdEstadoVacacion);


                            connection.Open();
                            cmd.ExecuteNonQuery();
                            //var id = (int)paramId.Value;
                            //MessageBox.Show("ID Retornado" + id);
                            connection.Close();
                        }
                        scope2.Complete();

                    }

                    using (TransactionScope scope3 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //buscar el registro de vacaciones saldos
                        using (SqlConnection connection = new SqlConnection(oConn.Conn()))

                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter da = new SqlDataAdapter("spSelVacacionesSaldosId", connection);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                da.SelectCommand.Parameters.AddWithValue("@Ano", Ano);
                                da.SelectCommand.Parameters.AddWithValue("@Mes", Mes);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                connection.Close();
                                //return dt;
                                if (IdEstadoVacacion == 2)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Actualizar
                                        //numRegVacSaldos = (int)dt.Rows[0][4];
                                        var cellValue = dt.Rows[0][4];
                                        // MessageBox.Show("Vacaciones saldos: " + cellValue);
                                        numRegVacSaldos = (decimal)cellValue + 1;

                                        // MessageBox.Show("Vacaciones saldos: " + numRegVacSaldos);


                                        using (SqlConnection connection1 = new SqlConnection(oConn.Conn()))

                                        {

                                            //SqlCommand cmd = new SqlCommand(query, connection);
                                            SqlCommand cmd = new SqlCommand("spUpdVacacionesSaldos", connection1);
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                                            //paramId.Direction = ParameterDirection.Output;


                                            //cmd.Parameters.Add(paramId);
                                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                            cmd.Parameters.AddWithValue("@Ano", Ano);
                                            cmd.Parameters.AddWithValue("@Mes", Mes);
                                            cmd.Parameters.AddWithValue("@VacacionesTomadas", numRegVacSaldos);



                                            connection1.Open();
                                            cmd.ExecuteNonQuery();
                                            //var id = (int)paramId.Value;
                                            //MessageBox.Show("ID Retornado" + id);
                                            connection1.Close();
                                            scope3.Complete();
                                        }




                                    }
                                    else
                                    {
                                        //insertar

                                        numRegVacSaldos++;

                                        using (SqlConnection connection2 = new SqlConnection(oConn.Conn()))

                                        {

                                            //SqlCommand cmd = new SqlCommand(query, connection);
                                            SqlCommand cmd = new SqlCommand("spInsVacacionesSaldos", connection2);
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                                            //paramId.Direction = ParameterDirection.Output;

                                            decimal vacAcum = (decimal)2.5;
                                            //cmd.Parameters.Add(paramId);
                                            cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                            cmd.Parameters.AddWithValue("@Ano", Ano);
                                            cmd.Parameters.AddWithValue("@Mes", Mes);
                                            cmd.Parameters.AddWithValue("@VacacionesAcumuladas", vacAcum);
                                            cmd.Parameters.AddWithValue("@VacacionesTomadas", numRegVacSaldos);



                                            connection2.Open();
                                            cmd.ExecuteNonQuery();
                                            //var id = (int)paramId.Value;
                                            //MessageBox.Show("ID Retornado" + id);
                                            connection2.Close();
                                            scope3.Complete();
                                        }
                                    }
                                }
                                else
                                {
                                    var cellValue = dt.Rows[0][4];
                                    // MessageBox.Show("Vacaciones saldos: " + cellValue);
                                    numRegVacSaldos = (decimal)cellValue - 1;


                                    using (SqlConnection connection1 = new SqlConnection(oConn.Conn()))

                                    {

                                        //SqlCommand cmd = new SqlCommand(query, connection);
                                        SqlCommand cmd = new SqlCommand("spUpdVacacionesSaldos", connection1);
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        //var paramId = new SqlParameter("@Id", SqlDbType.Int);
                                        //paramId.Direction = ParameterDirection.Output;


                                        //cmd.Parameters.Add(paramId);
                                        cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                                        cmd.Parameters.AddWithValue("@Ano", Ano);
                                        cmd.Parameters.AddWithValue("@Mes", Mes);
                                        cmd.Parameters.AddWithValue("@VacacionesTomadas", numRegVacSaldos);



                                        connection1.Open();
                                        cmd.ExecuteNonQuery();
                                        //var id = (int)paramId.Value;
                                        //MessageBox.Show("ID Retornado" + id);
                                        connection1.Close();
                                        scope3.Complete();
                                    }



                                }




                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Hay un error en la BD: " + ex.Message);

                            }

                        }




                    }

                    /*
                    using (TransactionScope scope4 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //...  
                    }


                    using (TransactionScope scope5 = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //...  
                    }


                    using (TransactionScope scope4 = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        //...  
                    }
                    */
                    scope1.Complete();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }



















    }
}
