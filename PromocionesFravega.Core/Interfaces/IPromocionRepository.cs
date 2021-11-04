using PromocionesFravega.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PromocionesFravega.Core.Interfaces
{
    public interface IPromocionRepository
    {
        Task<IEnumerable<Promocion>> GetPromociones();
        Task<IEnumerable<Promocion>> GetPromociones(Expression<Func<Promocion, bool>> exp);
        Task<IEnumerable<Promocion>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto);
        Task<IEnumerable<Promocion>> GetPromocionesMediosDePago(IEnumerable<string> mediosDePago, DateTime FechaInicio, DateTime FechaFin);
        Task<IEnumerable<Promocion>> GetPromocionesBancos(IEnumerable<string> Bancos, DateTime FechaInicio, DateTime FechaFin);
        Task<IEnumerable<Promocion>> GetPromocionesCategorias(IEnumerable<string> CategoriasProductos, DateTime FechaInicio, DateTime FechaFin);
        Task<Promocion> GetPromocion(string id);      

        Task InsertarPromocion(Promocion promocion);
        Task<bool> ActualizarPromocion(Promocion promocion);
        Task<bool> EliminarPromocion(string id);
    }
}
