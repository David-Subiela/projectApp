using Clase5_proyecto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Services
{
    public interface IPilotoService
    {
        Task<IEnumerable<PilotoDto>> Get();
        Task<PilotoDto> GetById(int id);
        Task<PilotoDto> Add(PilotoInsertDto pilotoInsertDto);
        Task<PilotoDto> Update(int id, PilotoUpdateDto pilotoUpdateDto);
        Task<PilotoDto> Delete(int id);
    }
}
