using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVacacionesBackEnd.Models
{
    public class RegistroVacacion
    {
        [Key][Column(Order = 0)]
        public int IdEmpleado { get; set; }

        [Key][Column(Order = 1)][DataType(DataType.Date)] 
        public DateTime FechaVacacion { get; set; }
        [ForeignKey("IdEstadoVacacion")]
        public int IdEstadoVacacion { get; set; }

        public ICollection<EstadosVacacion> EstadosVacaciones { get; set; } = new List<EstadosVacacion>();

    }
}
