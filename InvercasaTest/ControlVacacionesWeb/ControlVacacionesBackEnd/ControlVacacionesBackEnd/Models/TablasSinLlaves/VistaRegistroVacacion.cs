using Microsoft.EntityFrameworkCore;

namespace ControlVacacionesBackEnd.Models.TablasSinLlaves
{
    [Keyless]
    public class VistaRegistroVacacion
    {

        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }  
        public DateTime FechaVacacion { get; set; } 
        public string EstadosVacacion { get; set; }

    }
}
