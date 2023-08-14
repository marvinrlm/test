using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WindowsFormsApp1.Models
{
    public class Empleados
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public int IdTipoIdentificacion { get; set; }

        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Decimal SalarioBaseMensual { get; set; }
        public string Direccion { get; set; }
    }
}
