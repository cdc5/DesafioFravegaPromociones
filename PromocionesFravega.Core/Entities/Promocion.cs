using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.Entities
{
    public class Promocion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public IEnumerable<string> MediosDePago { get; private set; }
        public IEnumerable<string> Bancos { get; private set; }
        public IEnumerable<string> CategoriasProductos { get; private set; }
        public int? MaximaCantidadDeCuotas { get; private set; }
        public decimal? ValorInteresCuotas { get; private set; }
        public decimal? PorcentajeDeDescuento { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }

        public Promocion(string _Id, IEnumerable<string> _MediosDePago, IEnumerable<string> _Bancos, IEnumerable<string> _CategoriasProductos,
                        int? _MaximaCantidadDeCuotas, decimal? _ValorInteresCuotas, decimal? _PorcentajeDeDescuento, DateTime? _FechaInicio,
                        DateTime? _FechaFin, bool _Activo, DateTime _FechaCreacion, DateTime? _FechaModificacion)
        {
            Id = _Id;
            MediosDePago = _MediosDePago;
            Bancos = _Bancos;
            CategoriasProductos = _CategoriasProductos;
            MaximaCantidadDeCuotas = _MaximaCantidadDeCuotas;
            ValorInteresCuotas = _ValorInteresCuotas;
            PorcentajeDeDescuento = _PorcentajeDeDescuento;
            FechaInicio = _FechaInicio;
            FechaFin = _FechaFin;
            Activo = _Activo;
            FechaCreacion = _FechaCreacion;
            FechaModificacion = FechaModificacion;
        }

        public void SetActivo(bool _activo)
        {
            Activo = _activo;
        }

        public void SetFechaModificacion(DateTime _FechaModificacion)
        {
            FechaModificacion = FechaModificacion;
        }

        public void SetFechaCreacion(DateTime _FechaCreacion)
        {
            FechaCreacion = _FechaCreacion;
        }
        
    }
}
