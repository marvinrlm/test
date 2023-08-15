using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlVacacionesBackEnd.Models;

namespace ControlVacacionesBackEnd.Data
{
    public class ControlVacacionesBackEndContext : DbContext
    {
        public ControlVacacionesBackEndContext (DbContextOptions<ControlVacacionesBackEndContext> options)
            : base(options)
        {
        }

        public DbSet<ControlVacacionesBackEnd.Models.Empleado> Empleado { get; set; } = default!;

        public DbSet<ControlVacacionesBackEnd.Models.TiposIdentificacion> TiposIdentificacion { get; set; } = default!;
    }
}
