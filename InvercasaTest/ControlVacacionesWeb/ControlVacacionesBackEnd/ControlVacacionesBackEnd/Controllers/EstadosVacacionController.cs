using ControlVacacionesBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosVacacionController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private string? _connectionString;

        public EstadosVacacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<IEnumerable<EstadoVacacion>> Get()
        {
            var LstEstadoVacacion = new List<EstadoVacacion>();

            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("Select * from EstadoVacacion", connection))
                {
                    command.CommandType = CommandType.Text;

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LstEstadoVacacion.Add(new EstadoVacacion
                            {
                                Id = reader.GetInt32("Id"), 
                                EstadosVacacion = reader.GetString("EstadosVacacion")

                            });

                        }
                    }
                }

            }
            return LstEstadoVacacion;
        }


    }
}
