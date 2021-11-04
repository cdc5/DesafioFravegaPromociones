using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.QueryFilters
{
    public class PromocionesPorVentaQueryFilter
    {
        public string MedioDePago { get; set; }
        public string Banco { get; set; }
        public string CategoriaProducto { get; set; }
    }
}
