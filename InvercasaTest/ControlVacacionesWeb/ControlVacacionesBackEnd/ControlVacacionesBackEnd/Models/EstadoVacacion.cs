using System.ComponentModel.DataAnnotations;

namespace ControlVacacionesBackEnd.Models
{
    public class EstadoVacacion
    {
        public int Id { get; set; }
        [Required]
        public string EstadosVacacion { get; set; } = string.Empty;
    }
}
