using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class vVacacionesConsolidadas
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public decimal Acumulado { get; set; }
        public decimal Tomadas {  get; set; }
        public decimal Saldo { get; set; }
    }
}
