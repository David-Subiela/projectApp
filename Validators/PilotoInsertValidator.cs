using Clase5_proyecto.DTOs;
using FluentValidation;

namespace Clase5_proyecto.Validators
{
    public class PilotoInsertValidator : AbstractValidator<PilotoInsertDto>
    {
        public PilotoInsertValidator() 
        {
            RuleFor(x => x.HorasVueloAcumuladas).GreaterThanOrEqualTo(0).WithMessage("Las horas de vuelo acumuladas no pueden ser negativas.");
            RuleFor(x => x.NumeroLicencia).NotEmpty().WithMessage("La licencia no puede estar vacía.");            
            
        }
    }
}
