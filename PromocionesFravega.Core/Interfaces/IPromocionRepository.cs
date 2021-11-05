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
        Task<IEnumerable<Promocion>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto, DateTime fecha);
        Task<IEnumerable<Promocion>> GetPromocionesMediosDePago(IEnumerable<string> mediosDePago, DateTime FechaNuevaInicio, DateTime FechaNuevaFin);
        Task<IEnumerable<Promocion>> GetPromocionesBancos(IEnumerable<string> Bancos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin);
        Task<IEnumerable<Promocion>> GetPromocionesCategorias(IEnumerable<string> CategoriasProductos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin);
        Task<Promocion> GetPromocion(Guid id);      

        Task InsertarPromocion(Promocion promocion);
        Task<bool> ActualizarPromocion(Promocion promocion);
        Task<bool> EliminarPromocion(Guid id);
    }
}
