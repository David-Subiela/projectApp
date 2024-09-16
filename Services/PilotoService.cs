using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Clase5_proyecto.Services
{
    public class PilotoService : IPilotoService
    {

        private PlayContext _context;
        private IValidator<PilotoInsertDto> _pilotoInsertValidator;
        public PilotoService(PlayContext context, IValidator<PilotoInsertDto> pilotoInsertValidator)
        {
            _context = context;
            _pilotoInsertValidator = pilotoInsertValidator;
        }

        private PilotoDto MapToDto(Piloto piloto) => new PilotoDto
        {
            Id = piloto.Id,
            NombreCompleto = piloto.NombreCompleto,
            NumeroLicencia = piloto.NumeroLicencia,
            HorasVueloAcumuladas = piloto.HorasVueloAcumuladas,
            Disponibilidad = piloto.Disponibilidad,
        };

        // ------>>> GET <<<------
        public async Task<IEnumerable<PilotoDto>> Get() =>
            await _context.Pilotos.Select(p => new PilotoDto
            {
                Id = p.Id,
                NombreCompleto = p.NombreCompleto,
                NumeroLicencia = p.NumeroLicencia,
                HorasVueloAcumuladas = p.HorasVueloAcumuladas,
                Disponibilidad = p.Disponibilidad,
            }).ToListAsync();

        // ------>>> GET BY ID: <<<------
        public async Task<PilotoDto> GetById(int id)
        {
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto != null)
            {
                var pilotoFound = new PilotoDto
                {
                    Id = piloto.Id,
                    NombreCompleto = piloto.NombreCompleto,
                    NumeroLicencia = piloto.NumeroLicencia,
                    HorasVueloAcumuladas = piloto.HorasVueloAcumuladas,
                    Disponibilidad = piloto.Disponibilidad,
                };
                return pilotoFound;
            }
            return null;
        }

        // ------>>> ADD: POST ** AÑADIR NUEVOS REGISTROS A LA BBDD <<<------ 
        public async Task<PilotoDto> Add(PilotoInsertDto pilotoInsertDto)
        {
            var validationResult = await _pilotoInsertValidator.ValidateAsync(pilotoInsertDto);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var piloto = new Piloto()
            {
                //Id = pilotoInsertDto.Id,
                NombreCompleto = pilotoInsertDto.NombreCompleto,
                NumeroLicencia = pilotoInsertDto.NumeroLicencia,
                HorasVueloAcumuladas = pilotoInsertDto.HorasVueloAcumuladas,
                Disponibilidad = pilotoInsertDto.Disponibilidad,
            };

            await _context.Pilotos.AddAsync(piloto);
            await _context.SaveChangesAsync();

            return MapToDto(piloto);

        }

        // ------>>> UPDATE: PUT ** MODIFICAR POR ID <<<------
        public async Task<PilotoDto> Update(int id, PilotoUpdateDto pilotoUpdateDto)
        {
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto != null)
            {
                piloto.NombreCompleto = pilotoUpdateDto.NombreCompleto;
                piloto.NumeroLicencia = pilotoUpdateDto.NumeroLicencia;
                piloto.HorasVueloAcumuladas = pilotoUpdateDto.HorasVueloAcumuladas;                
                piloto.Disponibilidad = pilotoUpdateDto.Disponibilidad;

                await _context.SaveChangesAsync(); // Para mandar los datos a la BBDD.                             

                return MapToDto(piloto);
            }
            return null;
        }

        // ------>>> DELETE: <<<------
        public async Task<PilotoDto> Delete(int id)
        {
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto == null) return null;

            // Remover la aeronave del contexto
            _context.Pilotos.Remove(piloto);
            await _context.SaveChangesAsync();

            return MapToDto(piloto);
        }

    }
}
