using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PromocionesFravega.Core.Entities
{
    public partial class Stock
    {
        public string Deposito { get; set; }
        public string Area { get; set; }
        public string Pasillo { get; set; }
        public string Fila { get; set; }
        public string Cara { get; set; }
        public string ProductId { get; set; }
        public int Cantidad { get; set; }
    }
}
