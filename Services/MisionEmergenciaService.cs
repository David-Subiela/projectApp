using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clase5_proyecto.Services
{
    public class MisionEmergenciaService : IMisionEmergenciaService
    {

        private PlayContext _context;
        private IValidator<MisionEmergenciaInsertDto> _misionEmergenciaInsertValidator;
        public MisionEmergenciaService(PlayContext context, IValidator<MisionEmergenciaInsertDto> misionEmergenciaInsertValidator)
        {
            _context = context;
            _misionEmergenciaInsertValidator = misionEmergenciaInsertValidator;
        }

        private MisionEmergenciaDto MapToDto(MisionEmergencia misionEmergencia) => new MisionEmergenciaDto
        {
            Id = misionEmergencia.Id,
            TipoMision = misionEmergencia.TipoMision,
            date = misionEmergencia.date,
            Duracion = misionEmergencia.Duracion,
            Destino = misionEmergencia.Destino,            
        };

        // ------>>> GET <<<------
        public async Task<IEnumerable<MisionEmergenciaDto>> Get() =>
            await _context.MisionesEmergencia.Select(me => new MisionEmergenciaDto
            {
                Id = me.Id,
                TipoMision = me.TipoMision,
                date = me.date,
                Duracion = me.Duracion,
                Destino = me.Destino,                
            }).ToListAsync();

        // ------>>> GET BY ID: <<<------
        public async Task<MisionEmergenciaDto> GetById(int id)
        {
            var misionEmergencia = await _context.MisionesEmergencia.FindAsync(id);
            if (misionEmergencia != null)
            {
                var misionEmergenciaFound = new MisionEmergenciaDto
                {
                    Id = misionEmergencia.Id,
                    TipoMision = misionEmergencia.TipoMision,
                    date = misionEmergencia.date,
                    Duracion = misionEmergencia.Duracion,
                    Destino = misionEmergencia.Destino
                };
                return misionEmergenciaFound;
            }
            return null;
        }

        // ------>>> ADD: POST ** AÑADIR NUEVOS REGISTROS A LA BBDD <<<------ 
        public async Task<MisionEmergenciaDto> Add(MisionEmergenciaInsertDto misionEmergenciaInsertDto)
        {
            var validationResult = await _misionEmergenciaInsertValidator.ValidateAsync(misionEmergenciaInsertDto);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var misionEmergencia = new MisionEmergencia()
            {
                //Id = misionEmergenciaInsertDto.Id,
                TipoMision = misionEmergenciaInsertDto.TipoMision,
                date = misionEmergenciaInsertDto.date,
                Duracion = misionEmergenciaInsertDto.Duracion,
                Destino = misionEmergenciaInsertDto.Destino
            };

            await _context.MisionesEmergencia.AddAsync(misionEmergencia);
            await _context.SaveChangesAsync();            

            return MapToDto(misionEmergencia);

        }

        // ------>>> UPDATE: PUT ** MODIFICAR POR ID <<<------
        public async Task<MisionEmergenciaDto> Update(int id, MisionEmergenciaUpdateDto misionEmergenciaUpdateDto)
        {
            var misionEmergencia = await _context.MisionesEmergencia.FindAsync(id);
            if (misionEmergencia != null)
            {
                misionEmergencia.TipoMision = misionEmergenciaUpdateDto.TipoMision;
                misionEmergencia.date = misionEmergenciaUpdateDto.date;
                misionEmergencia.Duracion = misionEmergenciaUpdateDto.Duracion;
                misionEmergencia.Destino = misionEmergenciaUpdateDto.Destino;

                await _context.SaveChangesAsync(); // Para mandar los datos a la BBDD.                             

                return MapToDto(misionEmergencia);
            }
            return null;
        }

        // ------>>> DELETE: <<<------
        public async Task<MisionEmergenciaDto> Delete(int id)
        {
            var misionEmergencia = await _context.MisionesEmergencia.FindAsync(id);
            if (misionEmergencia == null) return null;

            // Remover la misión de emergencia del contexto
            _context.MisionesEmergencia.Remove(misionEmergencia);
            await _context.SaveChangesAsync();

            return MapToDto(misionEmergencia);
        }

    }
}
