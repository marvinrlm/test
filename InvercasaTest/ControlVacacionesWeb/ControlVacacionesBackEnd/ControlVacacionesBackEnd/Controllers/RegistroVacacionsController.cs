using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlVacacionesBackEnd.Data;
using ControlVacacionesBackEnd.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using ControlVacacionesBackEnd.Models.TablasSinLlaves;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroVacacionsController : ControllerBase
    {
        private readonly ControlVacacionesBackEndContext _context;
        private readonly IConfiguration _configuration;
        private string? _connectionString;

        public RegistroVacacionsController(ControlVacacionesBackEndContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RegistroVacacions
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroVacacion>>> GetRegistroVacacion()
        {
          if (_context.RegistroVacacion == null)
          {
              return NotFound();
          }
            return await _context.RegistroVacacion.ToListAsync();
        }*/

        [HttpGet]
        public  ActionResult<IEnumerable<VistaRegistroVacacion>> Get()
        {
            var LstRegistroVacacion = new List<VistaRegistroVacacion>();
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("Select * from VistaRegistroVacaciones", connection))
                {
                    command.CommandType = CommandType.Text;

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LstRegistroVacacion.Add(new VistaRegistroVacacion {
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                NombreCompleto = reader.GetString("NombreCompleto"),
                                FechaVacacion = reader.GetDateTime("FechaVacacion"),
                                EstadosVacacion = reader.GetString("EstadosVacacion")


                            
                            });
                        }
                    }

                }
                    
     
               
            }


            return LstRegistroVacacion;

        }


        [HttpGet("{IdEmpleado}")]

        public ActionResult<IEnumerable<VistaRegistroVacacion>> GetVistaRegistroVacacion(int IdEmpleado)
        {

            var LstVistaRegistroVacacion = new List <VistaRegistroVacacion>();
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");
            using(SqlConnection  connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM VistaRegistroVacaciones Where IdEmpleado =" + IdEmpleado,connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader()) {  
                        
                        while (reader.Read())
                        {
                            LstVistaRegistroVacacion.Add(new VistaRegistroVacacion
                            {
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                NombreCompleto = reader.GetString("NombreCompleto"),
                                FechaVacacion = reader.GetDateTime("FechaVacacion"),
                                EstadosVacacion = reader.GetString("EstadosVacacion")

                            });
                        } 
                    }

                }
            }
            return LstVistaRegistroVacacion;

        }

        [HttpGet("consolidadas")]

        public ActionResult<IEnumerable<VistaVacacionesConsolidadas>> GetVacacionesConsolidadas()
        {

            var LstVacacionesAcumuladas = new List<VistaVacacionesConsolidadas>();
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("Select * from VistaVacacionesConsolidadas",connection)) { 
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) {
                            LstVacacionesAcumuladas.Add(new VistaVacacionesConsolidadas
                            {
                                IdEmpleado = reader.GetInt32("IdEmpleado"),
                                NombreCompleto = reader.GetString("NombreCompleto"),
                                Acumulado = reader.GetDecimal("Acumulado"),
                                Tomadas = reader.GetDecimal("Tomadas"),
                                Saldo = reader.GetDecimal("Saldo")
                            });
                        }
                    }
                }
            }

            return LstVacacionesAcumuladas;
        }
        // GET: api/RegistroVacacions/5
       /* [HttpGet("{id}/{fecha}")]
        public async Task<ActionResult<RegistroVacacion>> GetRegistroVacacion(int id, DateTime fecha)
        {
          if (_context.RegistroVacacion == null)
          {
              return NotFound();
          }
            var registroVacacion = await _context.RegistroVacacion.FindAsync(id,fecha);

            if (registroVacacion == null)
            {
                return NotFound();
            }

            return registroVacacion;
        }*/

        // PUT: api/RegistroVacacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /*[HttpPut("{id}/{fecha}")]
        public async Task<IActionResult> PutRegistroVacacion(int id,DateTime fecha, RegistroVacacion registroVacacion)
        {
            if ((id != registroVacacion.IdEmpleado) && ( fecha != registroVacacion.FechaVacacion))
            {
                return BadRequest();
            }

            _context.Entry(registroVacacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroVacacionExists(id,fecha))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */
        [HttpPut("{IdEmpleado}/{FechaVacacion}/{IdEstadoVacacion}")]
        public async Task<ActionResult<RegistroVacacion>> PutRegistroVacacion(int IdEmpleado, DateTime FechaVacacion, int IdEstadoVacacion)
        {
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spUpdRegistroVacacionYsaldos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    command.Parameters.AddWithValue("@FechaVacacion", FechaVacacion);
                    command.Parameters.AddWithValue("@IdEstadoVacacion",IdEstadoVacacion);

                    int result;
                    result = await command.ExecuteNonQueryAsync();

                    return Ok(result);

                }
            }
        }

        // POST: api/RegistroVacacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<RegistroVacacion>> PostRegistroVacacion(RegistroVacacion registroVacacion)
        {
          if (_context.RegistroVacacion == null)
          {
              return Problem("Entity set 'ControlVacacionesBackEndContext.RegistroVacacion'  is null.");
          }
            _context.RegistroVacacion.Add(registroVacacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistroVacacionExists(registroVacacion.IdEmpleado,registroVacacion.FechaVacacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRegistroVacacion", new { id = registroVacacion.IdEstadoVacacion }, registroVacacion);
        }*/
        [HttpPost]

        public async Task<ActionResult<RegistroVacacion>> PostRegistroVacacion(RegistroVacacion registroVacacion)
        {
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spInsRegistroVacacionYsaldos", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", registroVacacion.IdEmpleado);
                    command.Parameters.AddWithValue("@FechaVacacion", registroVacacion.FechaVacacion);
                    command.Parameters.AddWithValue("@IdEstadoVacacion", registroVacacion.IdEstadoVacacion);

                    int result;
                    result = await command.ExecuteNonQueryAsync();

                    return Ok(result);
                
                }
            }

        }


        // DELETE: api/RegistroVacacions/5
        [HttpDelete("{id}/fecha")]
        public async Task<IActionResult> DeleteRegistroVacacion(int id, DateTime fecha)
        {
            if (_context.RegistroVacacion == null)
            {
                return NotFound();
            }
            var registroVacacion = await _context.RegistroVacacion.FindAsync(id, fecha);
            if (registroVacacion == null)
            {
                return NotFound();
            }

            _context.RegistroVacacion.Remove(registroVacacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroVacacionExists(int id, DateTime fecha)
        {
            return (_context.RegistroVacacion?.Any(e => e.IdEmpleado == id && e.FechaVacacion == fecha)).GetValueOrDefault();
        }
    }
}
