using FluentValidation;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;
namespace PromocionesFravega.Infrastructure.Validators
{
    class PromocionVigenciaUpdateDto : AbstractValidator<PromocionUpdateDto>
    {
        public PromocionVigenciaUpdateDto() 
        {
            //Cantidad de cuotas y porcentaje de descuento son nullables pero almenos una debe tener valor
            RuleFor(e => e.PorcentajeDeDescuento).NotNull().When(e => e.MaximaCantidadDeCuotas == null);
            RuleFor(e => e.MaximaCantidadDeCuotas).NotNull().When(e => e.PorcentajeDeDescuento == null);

            //La promoción puede tener porcentaje de descuento o cuotas. NO ambas
            RuleFor(e => e.PorcentajeDeDescuento).Null().When(e => e.MaximaCantidadDeCuotas != null);
            RuleFor(e => e.MaximaCantidadDeCuotas).Null().When(e => e.PorcentajeDeDescuento != null);

            //Porcentaje descuento en caso de tener valor, debe estar comprendido entre 5 y 80
            RuleFor(e => e.PorcentajeDeDescuento).ExclusiveBetween(5, 80);

            //Porcentaje interés cuota solo puede tener valor si cantidad de cuotas tiene valor
            RuleFor(e => e.ValorInteresCuotas).Null().When(e => e.MaximaCantidadDeCuotas == null);

            //Fecha fin no puede ser mayor que fecha inicio
            RuleFor(e => e.FechaInicio).LessThan(e => e.FechaFin);
        }
    }
}
