namespace Clase5_proyecto.DTOs
{
    public class AeronaveDto
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Model { get; set; }
        public int CapacidadCarga { get; set; }
        public int HorasVuelo { get; set; }
        public bool Disponibilidad { get; set; }
    }
}
