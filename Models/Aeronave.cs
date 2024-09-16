using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clase5_proyecto.Models
{
    public class Aeronave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        
        //[Required(ErrorMessage = "El Modelo no puede estar vacío")]
        public string? Model { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage = "La carga debe ser mayor a 0.")]
        public int CapacidadCarga { get; set; }

        //[Range(0, double.MaxValue, ErrorMessage = "Las horas de vuelo no pueden ser negativas.")]
        public int HorasVuelo { get; set; }
        public bool Disponibilidad { get; set; }

        public virtual IEnumerable<MisionEmergencia>? MisionesEmergencia { get; set; }

    }
}
