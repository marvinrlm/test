using ControlVacacionesBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Common;
using System.Data;
using System.Drawing;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposIdentificacionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string? _connectionString;
        public TiposIdentificacionController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]

        public ActionResult<IEnumerable<TiposIdentificacion>> CargarComboBoxTipoIdentificacion()
        {


            var lstTipoIdentificacion = new List<TiposIdentificacion>();

            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");
            using (SqlConnection connection = new SqlConnection(_connectionString))

            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * from TiposIdentificacion", connection)) {
                        command.CommandType = CommandType.Text;

                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {

                                lstTipoIdentificacion.Add(new TiposIdentificacion() { 
                                    Id = reader.GetInt32("id"), 
                                    TipoIdentificacion = reader.GetString("TipoIdentificacion")
                                });
 
                            }

                              
                        }
                        


                    }

                    return lstTipoIdentificacion;
  
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD: " + ex.Message);

                }

            }

        }








    }
}
