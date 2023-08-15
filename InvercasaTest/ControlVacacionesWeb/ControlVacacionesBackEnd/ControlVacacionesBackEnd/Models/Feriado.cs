using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControlVacacionesBackEnd.Models
{
    [Index(nameof(FechaFeriado), IsUnique = true)]
    public class Feriado
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        
        public DateTime FechaFeriado { get; set; }
        public string DescripcionFeriado { get; set; }=string.Empty;
    }
}
