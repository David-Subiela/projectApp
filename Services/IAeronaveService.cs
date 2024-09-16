using Clase5_proyecto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Services
{
    public interface IAeronaveService
    {
        Task<IEnumerable<AeronaveDto>> Get();
        Task<AeronaveDto> GetById(int id);
        Task<AeronaveDto> Add(AeronaveInsertDto aeronaveInsertDto);
        Task<AeronaveDto> Update(int id, AeronaveUpdateDto aeronaveUpdateDto);
        Task<AeronaveDto> Delete(int id);
    }
}
