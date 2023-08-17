using System.ComponentModel.DataAnnotations;

namespace ControlVacacionesBackEnd.Models
{
    public class TiposIdentificacion
    {

        public int Id { get; set; }
        [Required]
        public string TipoIdentificacion { get; set; } = string.Empty;
    }
}