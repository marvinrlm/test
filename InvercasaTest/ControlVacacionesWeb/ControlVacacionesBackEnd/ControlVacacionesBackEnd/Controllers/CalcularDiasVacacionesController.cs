using ControlVacacionesBackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using System.Data;
using System.Runtime.CompilerServices;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    
    public class CalcularDiasVacacionesController : ControllerBase
    {
        private readonly ControlVacacionesBackEndContext _context;
        private readonly IConfiguration _configuration;

        private string? _connectionString;

        public CalcularDiasVacacionesController(ControlVacacionesBackEndContext context, IConfiguration configuration)
        {
            _context = context; 
            _configuration = configuration;
        }


        // [HttpGet("{FIni:datetime}/{FFin:datetime}")]
        [HttpGet]
        public  IActionResult Get(DateTime FIni, DateTime FFin)
        {
            //string result= "Fecha Inicio: " + FIni + " FechaFinal: " + FFin ;
            //Console.WriteLine(result);
            //return  result;

            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                using(SqlCommand command = new SqlCommand($"Select dbo.fnCalcularVacaciones (@FechaInicio, @FechaFin)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@FechaInicio", FIni);
                    command.Parameters.AddWithValue("@FechaFin", FFin);


                    decimal result;

                    result = (decimal)command.ExecuteScalar();

                    return Ok((decimal)result);

                    //return Ok(result);


                }
            }
        }

    }
}
