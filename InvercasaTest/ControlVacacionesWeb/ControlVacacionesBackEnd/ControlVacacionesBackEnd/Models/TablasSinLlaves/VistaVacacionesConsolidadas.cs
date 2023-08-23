using Microsoft.EntityFrameworkCore;

namespace ControlVacacionesBackEnd.Models.TablasSinLlaves
{
    [Keyless]
    public class VistaVacacionesConsolidadas
    {
        public int IdEmpleado { get; set; } 
        public string NombreCompleto { get; set; }  
        public decimal Acumulado { get; set; }
        public decimal Tomadas { get; set; }
        public decimal Saldo { get; set; }

    }
}
