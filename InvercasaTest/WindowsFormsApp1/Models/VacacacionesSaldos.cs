using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class VacacacionesSaldos
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; } 
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal VacacionesAcumuladas { get; set; }
        public decimal VacacionesTomadas { get; set; }
    }
}
