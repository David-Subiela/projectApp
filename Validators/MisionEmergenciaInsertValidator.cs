using Azure;
using Clase5_proyecto.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Clase5_proyecto.Validators
{
    public class MisionEmergenciaInsertValidator : AbstractValidator<MisionEmergenciaInsertDto>
    {
        public MisionEmergenciaInsertValidator()
        {
            RuleFor(x => x.date).GreaterThanOrEqualTo(DateTime.Now).WithMessage("La Fecha de misión debe ser futura.");
            RuleFor(x => x.Piloto.Disponibilidad).Must(d=>d).WithMessage("El piloto debe estar disponible."); // d=>d (está validando que la Disponibilidad sea true. Es igual a poner: disponibilidad => disponibilidad == true).
            RuleFor(x => x.Aeronave.Disponibilidad).Must(d => d).WithMessage("La aeronave debe estar disponible.");            
        }
    }
}
