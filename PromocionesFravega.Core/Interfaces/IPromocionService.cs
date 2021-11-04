using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Interfaces;
using System.Linq;
using PromocionesFravega.Core.Exceptions;

namespace PromocionesFravega.Core.Interfaces
{
    public interface IPromocionService
    {
        
        Task<IEnumerable<Promocion>> GetPromociones();
        Task<Promocion> GetPromocion(Guid id);

        Task<IEnumerable<Promocion>> GetPromocionesVigentes();
        Task<IEnumerable<Promocion>> GetPromocionesVigentes(DateTime Fecha);
        Task<IEnumerable<PromocionVigenteDto>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto);
        Task<Guid> CrearPromocion(PromocionDto promocion);
        Task<Guid> ActualizarPromocion(PromocionUpdateDto promocion);
        Task<Guid> ActualizarPromocion(PromocionVigenciaUpdateDto promocionUpdDto);
        Task<Guid> EliminarPromocion(Guid id);
    }
}
