using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlVacacionesBackEnd.Data;
using ControlVacacionesBackEnd.Models;

namespace ControlVacacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposIdentificacionesController : ControllerBase
    {
        private readonly ControlVacacionesBackEndContext _context;

        public TiposIdentificacionesController(ControlVacacionesBackEndContext context)
        {
            _context = context;
        }

        // GET: api/TiposIdentificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposIdentificacion>>> GetTiposIdentificacion()
        {
          if (_context.TiposIdentificacion == null)
          {
              return NotFound();
          }
            return await _context.TiposIdentificacion.ToListAsync();
        }

        // GET: api/TiposIdentificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TiposIdentificacion>> GetTiposIdentificacion(int id)
        {
          if (_context.TiposIdentificacion == null)
          {
              return NotFound();
          }
            var tiposIdentificacion = await _context.TiposIdentificacion.FindAsync(id);

            if (tiposIdentificacion == null)
            {
                return NotFound();
            }

            return tiposIdentificacion;
        }

        // PUT: api/TiposIdentificaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiposIdentificacion(int id, TiposIdentificacion tiposIdentificacion)
        {
            if (id != tiposIdentificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(tiposIdentificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposIdentificacionExists(id))
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

        // POST: api/TiposIdentificaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TiposIdentificacion>> PostTiposIdentificacion(TiposIdentificacion tiposIdentificacion)
        {
          if (_context.TiposIdentificacion == null)
          {
              return Problem("Entity set 'ControlVacacionesBackEndContext.TiposIdentificacion'  is null.");
          }
            _context.TiposIdentificacion.Add(tiposIdentificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiposIdentificacion", new { id = tiposIdentificacion.Id }, tiposIdentificacion);
        }

        // DELETE: api/TiposIdentificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiposIdentificacion(int id)
        {
            if (_context.TiposIdentificacion == null)
            {
                return NotFound();
            }
            var tiposIdentificacion = await _context.TiposIdentificacion.FindAsync(id);
            if (tiposIdentificacion == null)
            {
                return NotFound();
            }

            _context.TiposIdentificacion.Remove(tiposIdentificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiposIdentificacionExists(int id)
        {
            return (_context.TiposIdentificacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
