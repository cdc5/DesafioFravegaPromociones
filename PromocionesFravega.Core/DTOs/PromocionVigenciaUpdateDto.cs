using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.DTOs
{
    public class PromocionVigenciaUpdateDto
    {
        public Guid Id { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }        
    }
}
