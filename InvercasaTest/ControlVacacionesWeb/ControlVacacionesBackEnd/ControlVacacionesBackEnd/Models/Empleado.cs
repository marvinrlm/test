using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVacacionesBackEnd.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
       
        [ForeignKey("IdTipoIdentificacion")]
        public int IdTipoIdentificacion { get; set; }

    
        [Required]
        public string NumeroIdentificacion { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioBaseMensual { get; set; } = decimal.Zero;
        [Required]
        public string Direccion { get; set; } = string.Empty;

        //public string TipoIdentificacion { get; } = string.Empty;

       // public ICollection<TiposIdentificacion> TiposIdentificaciones { get; set; } = new List<TiposIdentificacion>();

    }
}