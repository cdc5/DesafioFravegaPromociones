using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.DTOs
{
    public class PromocionUpdateDto
    {
        public Guid Id { get;  set; }
        public IEnumerable<string> MediosDePago { get;  set; }
        public IEnumerable<string> Bancos { get;  set; }
        public IEnumerable<string> CategoriasProductos { get;  set; }
        public int? MaximaCantidadDeCuotas { get;  set; }
        public decimal? ValorInteresCuotas { get;  set; }
        public decimal? PorcentajeDeDescuento { get;  set; }
        public DateTime? FechaInicio { get;  set; }
        public DateTime? FechaFin { get;  set; }
        public bool Activo { get;  set; }     
    }
}
