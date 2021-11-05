using PromocionesFravega.Infrastructure.Data.Interfaces;
using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace PromocionesFravega.Infrastructure.Repositories
{
    public class PromocionRepository : IPromocionRepository
    {
        private readonly IPromocionContext _context;

        public PromocionRepository(IPromocionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Promocion>> GetPromociones()
        {
            return await _context
                            .Promociones
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Promocion> GetPromocion(Guid id)
        {
            return await _context
                           .Promociones
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Promocion>> GetPromociones(Expression<Func<Promocion,bool>> exp)
        {
            FilterDefinition<Promocion> filter = Builders<Promocion>.Filter.Where(exp);
            return await _context
                            .Promociones
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesMediosDePago(IEnumerable<string> mediosDePago, DateTime FechaNuevaInicio,DateTime FechaNuevaFin)
        {
            FilterDefinition<Promocion> Fechafilter = Builders<Promocion>.Filter.Where(x=> ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio<= FechaNuevaFin)
                                                                                  ||  (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin)));
            FilterDefinition<Promocion> Activofilter = Builders<Promocion>.Filter.Where(x => x.Activo == true);

            FilterDefinition<Promocion> filter = Fechafilter & Activofilter;

            if (mediosDePago.Count() > 0)
            {
                FilterDefinition<Promocion> MedioDePagofilter = Builders<Promocion>.Filter.ElemMatch(o => o.MediosDePago, d => mediosDePago.Contains(d));
                filter = filter & MedioDePagofilter;
            }
            
            return await _context.Promociones.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesBancos(IEnumerable<string> Bancos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin)
        {
            FilterDefinition<Promocion> Fechafilter = Builders<Promocion>.Filter.Where(x => ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio <= FechaNuevaFin)
                                                                                   || (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin)));
            FilterDefinition<Promocion> Activofilter = Builders<Promocion>.Filter.Where(x => x.Activo == true);

            FilterDefinition<Promocion> filter = Fechafilter & Activofilter;

            if (Bancos.Count() > 0)
            {
                FilterDefinition<Promocion> Bancofilter = Builders<Promocion>.Filter.ElemMatch(o => o.Bancos, d => Bancos.Contains(d));
                filter = filter & Bancofilter;
            }            
            
            return await _context.Promociones.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesCategorias(IEnumerable<string> CategoriasProductos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin)
        {
            FilterDefinition<Promocion> Fechafilter = Builders<Promocion>.Filter.Where(x => ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio <= FechaNuevaFin)
                                                                                 || (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin)));
            FilterDefinition<Promocion> Activofilter = Builders<Promocion>.Filter.Where(x => x.Activo == true);

            FilterDefinition<Promocion> filter = Fechafilter & Activofilter;

            if (CategoriasProductos.Count() > 0)
            {
                FilterDefinition<Promocion> CategoriasFilter = Builders<Promocion>.Filter.ElemMatch(o => o.CategoriasProductos, d => CategoriasProductos.Contains(d));
                filter = filter & CategoriasFilter;
            }            
            return await _context.Promociones.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto,DateTime fecha)
        {
            var listaMedio = new List<string> { medioDePago };
            var listaBanco = new List<string> { Banco };
            //var listaCategorias = categoriasProductos.ToList();
            var listaCategorias = new List<string> { categoriaProducto };

            FilterDefinition<Promocion> MedioDePagofilter = Builders<Promocion>.Filter.ElemMatch(o=>o.MediosDePago, d=> listaMedio.Contains(d));
            FilterDefinition<Promocion> Bancofilter = Builders<Promocion>.Filter.ElemMatch(o => o.Bancos, d => listaBanco.Contains(d));
            FilterDefinition<Promocion> CategoriasFilter = Builders<Promocion>.Filter.ElemMatch(o => o.CategoriasProductos, d => listaCategorias.Contains(d));
            FilterDefinition<Promocion> FechaIniciofilter = Builders<Promocion>.Filter.Gte(p => p.FechaInicio, fecha);
            FilterDefinition<Promocion> FechaFinfilter = Builders<Promocion>.Filter.Lte(p => p.FechaFin, fecha);
            FilterDefinition<Promocion> Activofilter = Builders<Promocion>.Filter.Where(x => x.Activo == true);

            var filter = MedioDePagofilter & Bancofilter & CategoriasFilter & FechaIniciofilter & FechaFinfilter & Activofilter;
            return await _context.Promociones.Find(filter).ToListAsync();
        }

        //public async Task<IEnumerable<Product>> GetProductByName(string name)
        //{
        //    FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

        //    return await _context
        //                    .Promociones
        //                    .Find(filter)
        //                    .ToListAsync();
        //}

        //public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        //{
        //    FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        //    return await _context
        //                    .Products
        //                    .Find(filter)
        //                    .ToListAsync();
        //}


        public async Task InsertarPromocion(Promocion promocion)
        {
            await _context.Promociones.InsertOneAsync(promocion);
        }

        public async Task<bool> ActualizarPromocion(Promocion promocion)
        {
            var actResult = await _context
                                        .Promociones
                                        .ReplaceOneAsync(filter: g => g.Id == promocion.Id, replacement: promocion);

            return actResult.IsAcknowledged
                    && actResult.ModifiedCount > 0;
        }

       
        public async Task<bool> EliminarPromocion(Guid id)
        {
            FilterDefinition<Promocion> filter = Builders<Promocion>.Filter.Eq(p => p.Id, id);

            DeleteResult elimResult = await _context
                                                .Promociones
                                                .DeleteOneAsync(filter);

            return elimResult.IsAcknowledged
                && elimResult.DeletedCount > 0;
        }
    }
}
