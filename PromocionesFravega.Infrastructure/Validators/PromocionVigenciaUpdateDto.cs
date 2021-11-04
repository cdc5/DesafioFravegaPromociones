using FluentValidation;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;
namespace PromocionesFravega.Infrastructure.Validators
{
    class PromocionVigenciaUpdateDto : AbstractValidator<PromocionUpdateDto>
    {
        public PromocionVigenciaUpdateDto() 
        {
            RuleFor(e => e.FechaFin).LessThan(e => e.FechaInicio);
        }
    }
}
