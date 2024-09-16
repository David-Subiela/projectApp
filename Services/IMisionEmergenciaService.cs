using Clase5_proyecto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Services
{
    public interface IMisionEmergenciaService
    {
        Task<IEnumerable<MisionEmergenciaDto>> Get();
        Task<MisionEmergenciaDto> GetById(int id);
        Task<MisionEmergenciaDto> Add(MisionEmergenciaInsertDto misionEmergenciaInsertDto);
        Task<MisionEmergenciaDto> Update(int id, MisionEmergenciaUpdateDto misionEmergenciaUpdateDto);
        Task<MisionEmergenciaDto> Delete(int id);
    }
}
