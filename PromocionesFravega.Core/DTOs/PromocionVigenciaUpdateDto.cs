using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.DTOs
{
    public class PromocionVigenciaUpdateDto
    {
        public string Id { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }        
    }
}
