namespace Clase5_proyecto.DTOs
{
    public class PilotoInsertDto
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NumeroLicencia { get; set; }        
        public int HorasVueloAcumuladas { get; set; }
        public bool Disponibilidad { get; set; }
    }
}
