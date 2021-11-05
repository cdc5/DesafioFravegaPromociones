using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.DTOs
{
    public class PromocionVigenciaUpdateDto
    {
        public Guid Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }        
    }
}
