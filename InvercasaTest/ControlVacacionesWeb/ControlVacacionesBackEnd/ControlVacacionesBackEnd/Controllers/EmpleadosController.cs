using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlVacacionesBackEnd.Data;
using ControlVacacionesBackEnd.Models;
using ControlVacacionesBackEnd.Models.TablasSinLlaves;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Collections;
using System.Data.SqlTypes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Metadata;
using System.Data.Common;
using System.Security.Cryptography;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ControlVacacionesBackEndContext _context;
        private readonly IConfiguration _configuration;

        private string? _connectionString;




        public EmpleadosController(ControlVacacionesBackEndContext context, IConfiguration configuration)
        {
            _context = context;  //Para usar con Entity Framework
            _configuration = configuration; //Para usar con procedimientos almacenados
        }


        // GET: api/Empleados
        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleado()
         {
           if (_context.Empleado == null)
           {
               return NotFound();
           }
             return await _context.Empleado.ToListAsync();
         }*/



        /* [HttpGet]
         public  IActionResult GetEmpleadosFromView()
         {
             var empleadosFromView =  _context.Set<VistaEmpleados>().FromSqlRaw("SELECT * FROM VistaEmpleados").ToList();
             return Ok(empleadosFromView);
         }*/

        //[HttpGet("asyncsale")]
        [HttpGet]
        public async IAsyncEnumerable<VistaEmpleados> GetEmpleadosFromView()
        {
            var empleados = _context.Set<VistaEmpleados>().OrderBy(p => p.NombreCompleto).AsAsyncEnumerable();

            await foreach (var empleado in empleados)
            {

                    yield return empleado;

            }
        }




        // GET: api/Empleados/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
          if (_context.Empleado == null)
          {
              return NotFound();
          }
            var empleado = await _context.Empleado.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }*/


        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {


            
            var empleado = await _context.Empleado
                
                .FromSqlRaw($"EXECUTE spSelEmpleadoId @Id={id};")
                
                .ToListAsync();

            //var empleado1 = await _context.Database.ExecuteSqlAsync<Empleado>(@"")
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }





        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return BadRequest();
            }

            //_context.Entry(empleado).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();

                _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

                using (SqlConnection connection = new SqlConnection(_connectionString)) { 
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spUpdEmpleado", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            //Direction = ParameterDirection.Output,
                             Value = id

                        });

                        command.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                        command.Parameters.AddWithValue("@IdTipoIdentificacion", empleado.IdTipoIdentificacion);
                        command.Parameters.AddWithValue("@NumeroIdentificacion", empleado.NumeroIdentificacion);
                        command.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                        command.Parameters.AddWithValue("@SalarioBaseMensual", empleado.SalarioBaseMensual);
                        command.Parameters.AddWithValue("@Direccion", empleado.Direccion);

                        int result;

                        result = await command.ExecuteNonQueryAsync();

                        return Ok(result);


                    }

                }
                




            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
        }

        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
          if (_context.Empleado == null)
          {
              return Problem("Entity set 'ControlVacacionesBackEndContext.Empleado'  is null.");
          }
            _context.Empleado.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = empleado.Id }, empleado);
        }
        */


        [HttpPost]
        //public  Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        public async Task<ActionResult> PostEmpleado(Empleado empleado)
        {

           
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spInsEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output,
                            // Value = new SqlXml(idList.CreateReader())
                   
                        });

                command.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                command.Parameters.AddWithValue("@IdTipoIdentificacion", empleado.IdTipoIdentificacion);
                command.Parameters.AddWithValue("@NumeroIdentificacion", empleado.NumeroIdentificacion);
                command.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                command.Parameters.AddWithValue("@SalarioBaseMensual", empleado.SalarioBaseMensual);
                command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                
                //command.ExecuteNonQuery();


                   await using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            var newRowID = Convert.ToInt32(reader["Id"]);
                            reader.Close();
                            return Ok(newRowID);
                           
                        }
                        else
                        {
                            return Ok(null);
                        }

                        
                    }



                }

            }

        }




        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            if (_context.Empleado == null)
            {
                return NotFound();
            }
            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            /* _context.Empleado.Remove(empleado);
             await _context.SaveChangesAsync();*/
            _connectionString = _configuration.GetConnectionString("ControlVacacionesBackEndContext");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("spDelEmpleado", connection))
                {
                    //command.Parameters.AddWithValue("@Id", id);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(
                        
                        new SqlParameter

                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            //Direction = ParameterDirection.Output,
                             Value = id,

                        });
                    command.ExecuteNonQuery();

                }
                
            }

            return NoContent();
        }








        private bool EmpleadoExists(int id)
        {
            return (_context.Empleado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
