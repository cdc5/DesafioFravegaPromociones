﻿using FluentValidation;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;

namespace PromocionesFravega.Infrastructure.Validators
{
    public class PromocionUpdateDtoValidator : AbstractValidator<PromocionUpdateDto>
    {
        public PromocionUpdateDtoValidator()
        {
            RuleFor(e => e.PorcentajeDeDescuento).NotNull().When(e => e.ValorInteresCuotas == null);
            RuleFor(e => e.ValorInteresCuotas).NotNull().When(e => e.PorcentajeDeDescuento == null);

            RuleFor(e => e.ValorInteresCuotas).Null().When(e => e.MaximaCantidadDeCuotas == null);
            RuleFor(e => e.PorcentajeDeDescuento).ExclusiveBetween(5, 80);

            RuleFor(e => e.FechaFin).LessThan(e => e.FechaInicio);
        }
    }
}
