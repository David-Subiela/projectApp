using AutoMapper;
using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;

namespace Clase5_proyecto.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<AeronaveInsertDto, Aeronave>();
            CreateMap<PilotoInsertDto, Piloto>();
            CreateMap<MisionEmergenciaInsertDto, MisionEmergencia>();
        }
    }
}
