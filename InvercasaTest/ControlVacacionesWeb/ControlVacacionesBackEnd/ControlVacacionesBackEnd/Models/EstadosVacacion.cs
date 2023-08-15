using System.ComponentModel.DataAnnotations;

namespace ControlVacacionesBackEnd.Models
{
    public class EstadosVacacion
    {
        public int Id { get; set; }
        [Required]
        public string EstadoVacacion { get; set; } = string.Empty;
    }
}
