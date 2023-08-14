using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class RegistrosVacaciones
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaVacacion { get; set; }
        public int IdEstadoVacacion {get; set; }
    }
}
