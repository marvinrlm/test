using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlVacacionesBackEnd.Models
{
    public class VacacionSaldo
    {
        [Key]
        [Column(Order = 0)]
        public int IdEmpleado { get; set; }

        [Key][Column(Order = 1)]
        public int Ano { get; set; }
        [Key][Column(Order = 2)]
        public int Mes { get; set; }
        [Required]
        [Column(TypeName="decimal(18,2)")]
        public decimal VacacionAcumulada { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal VacacionTomada { get; set; }


    }
}
