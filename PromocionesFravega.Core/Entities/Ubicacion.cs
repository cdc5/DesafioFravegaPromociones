using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.Entities
{
    public class Ubicacion
    {
        public string Area { get; set; }
        public string Pasillo { get; set; }
        public string Fila { get; set; }
        public string Cara { get; set; }

        public Ubicacion(string ubicacion)
        {
            var ubi = ubicacion.Split('-');
            Area = ubi[0];
            Pasillo = ubi[1];
            Fila = ubi[2];
            Cara = ubi[3];
        }

    }
}
