using Microsoft.EntityFrameworkCore;

namespace ControlVacacionesBackEnd.Models.TablasSinLlaves
{
    [Keyless]
    public class VistaEmpleados
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoIdentificacion { get; set;}
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal SalarioBaseMensual { get; set; }
        public string Direccion { get; set; }
        public int IdTipoIdentificacion { get; set; }

        
    }
}
