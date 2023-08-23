using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlVacacionesBackEnd.Models;
using ControlVacacionesBackEnd.Models.TablasSinLlaves;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VistaEmpleados>();
            /*.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);*/



            modelBuilder.Entity<RegistroVacacion>().HasKey(table => new
            {
                table.IdEmpleado,
                table.FechaVacacion
            });
        }

  
        public DbSet<RegistroVacacion> RegistroVacacion { get; set; }

        public DbSet<ControlVacacionesBackEnd.Models.EstadoVacacion> EstadoVacacion { get; set; } = default!;
    }
}
