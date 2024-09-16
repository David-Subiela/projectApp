using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clase5_proyecto.Models
{
    public class Piloto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NumeroLicencia { get; set; }
        public int HorasVueloAcumuladas { get; set; }
        public bool Disponibilidad { get; set; }

        public virtual IEnumerable<MisionEmergencia>? MisionesEmergencia { get; set; }
    }
}
