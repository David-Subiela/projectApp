using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Services
{
    public class AeronaveService : IAeronaveService
    {

        private PlayContext _context;
        private IValidator<AeronaveInsertDto> _aeronaveInsertValidator;
        public AeronaveService(PlayContext context, IValidator<AeronaveInsertDto> aeronaveInsertValidator)
        {
            _context = context;
            _aeronaveInsertValidator = aeronaveInsertValidator;
        }

        private AeronaveDto MapToDto(Aeronave aeronave) => new AeronaveDto
        {
            Id = aeronave.Id,
            Tipo = aeronave.Tipo,
            Model = aeronave.Model,
            CapacidadCarga = aeronave.CapacidadCarga,
            HorasVuelo = aeronave.HorasVuelo,
            Disponibilidad = aeronave.Disponibilidad,
        };

        // ------>>> GET <<<------
        public async Task<IEnumerable<AeronaveDto>> Get() =>
            await _context.Aeronaves.Select(a => new AeronaveDto
            {
                Id = a.Id,
                Tipo = a.Tipo,
                Model = a.Model,
                CapacidadCarga = a.CapacidadCarga,
                HorasVuelo = a.HorasVuelo,
                Disponibilidad = a.Disponibilidad,
            }).ToListAsync();

        // ------>>> GET BY ID: <<<------
        public async Task<AeronaveDto> GetById(int id)
        {
            var aeronave = await _context.Aeronaves.FindAsync(id);
            if (aeronave != null)
            {
                var aeronaveFound = new AeronaveDto
                {
                    Id = aeronave.Id,
                    Tipo = aeronave.Tipo,
                    Model = aeronave.Model,
                    CapacidadCarga = aeronave.CapacidadCarga,
                    HorasVuelo = aeronave.HorasVuelo,
                    Disponibilidad = aeronave.Disponibilidad,
                };
                return aeronaveFound;
            }
            return null;
        }

        // ------>>> ADD: POST ** AÑADIR NUEVOS REGISTROS A LA BBDD <<<------ 
        public async Task<AeronaveDto> Add(AeronaveInsertDto aeronaveInsertDto)
        {
            var validationResult = await _aeronaveInsertValidator.ValidateAsync(aeronaveInsertDto);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var aeronave = new Aeronave()
            {
                //Id = aeronaveInsertDto.Id,
                Tipo = aeronaveInsertDto.Tipo,
                Model = aeronaveInsertDto.Model,
                CapacidadCarga = aeronaveInsertDto.CapacidadCarga,
                HorasVuelo = aeronaveInsertDto.HorasVuelo,
                Disponibilidad = aeronaveInsertDto.Disponibilidad,
            };

            await _context.Aeronaves.AddAsync(aeronave);
            await _context.SaveChangesAsync();            

            return MapToDto(aeronave);

        }

        // ------>>> UPDATE: PUT ** MODIFICAR POR ID <<<------
        public async Task<AeronaveDto> Update(int id, AeronaveUpdateDto aeronaveUpdateDto)
        {
            var aeronave = await _context.Aeronaves.FindAsync(id);
            if (aeronave != null)
            {
                aeronave.Tipo = aeronaveUpdateDto.Tipo;
                aeronave.Model = aeronaveUpdateDto.Model;
                aeronave.CapacidadCarga = aeronaveUpdateDto.CapacidadCarga;
                aeronave.HorasVuelo = aeronaveUpdateDto.HorasVuelo;
                aeronave.Disponibilidad = aeronaveUpdateDto.Disponibilidad;

                await _context.SaveChangesAsync(); // Para mandar los datos a la BBDD.                             

                return MapToDto(aeronave);
            }
            return null;
        }

        // ------>>> DELETE: <<<------
        public async Task<AeronaveDto> Delete(int id)
        {
            var aeronave = await _context.Aeronaves.FindAsync(id);
            if (aeronave == null) return null;

            // Remover la aeronave del contexto
            _context.Aeronaves.Remove(aeronave);
            await _context.SaveChangesAsync();

            return MapToDto(aeronave);
        }

    }
}
