using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PromocionesFravega.UnitTests
{
    public class PromocionRepositoryMock : IPromocionRepository
    {
        private readonly List<Promocion> _promociones;

        //simula la completitud de una tarea asincronica, se hace una tarea en cache para optimizar la respuesta.
        public static readonly Task CompletedTask = Task.FromResult(false);
        public static readonly Task<Promocion> CompletedTaskPromociones = Task.FromResult(new Promocion());
        
        public PromocionRepositoryMock()
        {
            _promociones = new List<Promocion>()
            {
                //new Promocion() { Deposito = "AR01",Area = "LM",Pasillo = "00",Fila = "00",Cara = "IZ",ProductId = "MLA813727183",Cantidad = 50 },
                //new Stock() { Deposito = "AR01",Area = "LM",Pasillo = "00",Fila = "00",Cara = "IZ",ProductId = "MLA813727184",Cantidad = 8 },
                //new Stock() { Deposito = "AR01",Area = "LM",Pasillo = "00",Fila = "00",Cara = "IZ",ProductId = "MLA813727185",Cantidad = 14 },
                //new Stock() { Deposito = "AR01",Area = "LM",Pasillo = "04",Fila = "02",Cara = "IZ",ProductId = "MLA813727183",Cantidad = 28 },
                //new Stock() { Deposito = "AR01",Area = "LM",Pasillo = "01",Fila = "01",Cara = "DE",ProductId = "MLA813727183",Cantidad = 11 },
                //new Stock() { Deposito = "AR01",Area = "AL",Pasillo = "00",Fila = "00",Cara = "DE",ProductId = "MLA813727184",Cantidad = 21 },
                //new Stock() { Deposito = "AR01",Area = "AL",Pasillo = "00",Fila = "00",Cara = "IZ",ProductId = "MLA813727185",Cantidad = 13 },
            };
        }

        public Task<bool> ActualizarPromocion(Promocion promocion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarPromocion(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Promocion> GetPromocion(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromociones()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromociones(Expression<Func<Promocion, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromocionesBancos(IEnumerable<string> Bancos, DateTime FechaInicio, DateTime FechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromocionesCategorias(IEnumerable<string> CategoriasProductos, DateTime FechaInicio, DateTime FechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromocionesMediosDePago(IEnumerable<string> mediosDePago, DateTime FechaInicio, DateTime FechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Promocion>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto)
        {
            throw new NotImplementedException();
        }

        public Task InsertarPromocion(Promocion promocion)
        {
            throw new NotImplementedException();
        }
    }
}
