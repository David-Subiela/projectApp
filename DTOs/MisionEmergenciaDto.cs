using Clase5_proyecto.Models;

namespace Clase5_proyecto.DTOs
{
    public class MisionEmergenciaDto
    {
        public int Id { get; set; }
        public string? TipoMision { get; set; }

        public DateTime date { get; set; }
        public int Duracion { get; set; }

        public string? Destino { get; set; }
        public int AeronaveId { get; set; }
        public int PilotoId { get; set; }
        public virtual Aeronave? Aeronave { get; set; }
        public virtual Piloto? Piloto { get; set; }

    }
}
