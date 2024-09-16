using Azure;
using Clase5_proyecto.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Clase5_proyecto.Validators
{
    public class AeronaveInsertValidator : AbstractValidator<AeronaveInsertDto>
    {
        public AeronaveInsertValidator()
        {

            RuleFor(x => x.Model).NotEmpty().WithMessage("El Modelo no puede estar vacío.");
            RuleFor(x => x.CapacidadCarga).GreaterThan(0).WithMessage("La carga debe ser mayor a 0.");
            RuleFor(x => x.HorasVuelo).GreaterThanOrEqualTo(0).WithMessage("Las horas de vuelo no pueden ser negativas.");      
        }
    }
}
