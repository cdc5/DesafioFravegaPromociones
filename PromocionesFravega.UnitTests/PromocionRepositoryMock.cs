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
                //Vigente en todo noviembre y diciembre
                new Promocion(new Guid("219eaa64-25a4-41d5-9509-cd76d0138b42"),new List<string>(){"GIFT_CARD" },new List<string>(){"Macro"},
                             new List<string>(){ "Tecnologia", "Celulares", "Colchones"},12,null,null,DateTime.Parse("2021-11-01T03:00:00Z"),DateTime.Parse("2021-12-31T03:00:00Z")
                             ,true,DateTime.Parse("2021-11-04T19:40:09.782Z"),null),
                //Fecha para todo noviembre y diciembre, pero no está activa
                new Promocion(new Guid("f171183f-632f-4324-9354-b8d202651f62"),new List<string>(){"GIFT_CARD" },new List<string>(){"Macro"},
                             new List<string>(){ "Tecnologia", "Celulares", "Colchones"},null,null,20.0m,DateTime.Parse("2021-07-01T03:00:00Z"),DateTime.Parse("2021-07-30T03:00:00Z")
                             ,false,DateTime.Parse("2021-11-04T19:40:09.782Z"),null),
                 new Promocion(new Guid("4b46e3b6-89cc-42bc-bdd7-6fcae2f0af93"),new List<string>(){"TARJETA_CREDITO" },new List<string>(){"Santander Rio"},
                             new List<string>(){ "Jardin"},null,null,20.0m,DateTime.Parse("2021-07-01T03:00:00Z"),DateTime.Parse("2021-07-30T03:00:00Z")
                             ,true,DateTime.Parse("2021-11-04T19:40:09.782Z"),null),
                 new Promocion(new Guid("f66ddfd1-dc16-4b8d-ab69-79dc0925231e"),new List<string>(){"TARJETA_CREDITO" },new List<string>(){"ICBC"},
                             new List<string>(){ "Jardin","GrandesElectro"},1,1.0m,null,DateTime.Parse("2021-09-01T03:00:00Z"),DateTime.Parse("2021-09-30T03:00:00Z")
                             ,true,DateTime.Parse("2021-11-04T19:40:09.782Z"),null),
                 new Promocion(new Guid("2c3269af-e89c-4c97-ab40-964e5f222b08"),new List<string>(){"TARJETA_DEBITO" },new List<string>(){"Nacion"},
                             new List<string>(){ "Celulares", "Audio"},8,3.75m,null,DateTime.Parse("2021-09-01T03:00:00Z"),DateTime.Parse("2021-09-30T03:00:00Z")
                             ,true,DateTime.Parse("2021-11-04T19:40:09.782Z"),null)

            };
        }

        public Task<bool> ActualizarPromocion(Promocion promocion)
        {
            //simulamos actualizacion de los datos mediante una eliminación del objeto existente y un agregado del nuevo
            var promo = _promociones.Find(x => x.Id == promocion.Id);
            _promociones.Remove(promo);
            _promociones.Add(promocion);
            return Task.FromResult(true);
        }

        public Task<bool> EliminarPromocion(Guid id)
        {
            var promo = _promociones.Find(x => x.Id == id);
            _promociones.Remove(promo);
            return Task.FromResult(true);
        }

        public Task<Promocion> GetPromocion(Guid id)
        {
            var promo =_promociones.Find(x => x.Id == id);
            return Task.FromResult(promo);
        }

        public Task<IEnumerable<Promocion>> GetPromociones()
        {
            var promos = _promociones.ToList();
            return Task.FromResult((IEnumerable<Promocion>)promos);
        }

        public Task<IEnumerable<Promocion>> GetPromociones(Expression<Func<Promocion, bool>> filter)
        {
            IQueryable<Promocion> query = _promociones.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return Task.FromResult((IEnumerable<Promocion>)query.ToList());
            
            //var promos = _promociones.Where(exp.Compile()).ToList();
            //return (Task<IEnumerable<Promocion>>)(IEnumerable<Promocion>)promos;
        }
        public async Task<IEnumerable<Promocion>> GetPromocionesMediosDePago(IEnumerable<string> mediosDePago, DateTime FechaNuevaInicio, DateTime FechaNuevaFin)
        {
            List<Promocion> lista = new List<Promocion>();
            var promo = await GetPromociones(x => ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio <= FechaNuevaFin)
                                                || (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin))
                                                && x.Activo == true);

            var promos =  promo.Where(p => p.MediosDePago.Any(x => mediosDePago.Contains(x)));
            return promos;
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesBancos(IEnumerable<string> Bancos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin)
        {
            List<Promocion> lista = new List<Promocion>();
            var promo = await GetPromociones(x => ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio <= FechaNuevaFin)
                                                || (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin))
                                                && x.Activo == true);

            var promos = promo.Where(p => p.Bancos.Any(x => Bancos.Contains(x)));
            return promos;
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesCategorias(IEnumerable<string> CategoriasProductos, DateTime FechaNuevaInicio, DateTime FechaNuevaFin)
        {
            List<Promocion> lista = new List<Promocion>();
            var promo = await GetPromociones(x => ((x.FechaInicio >= FechaNuevaInicio && x.FechaInicio <= FechaNuevaFin)
                                                || (x.FechaFin >= FechaNuevaInicio && x.FechaFin <= FechaNuevaFin))
                                                && x.Activo == true);

            var promos = promo.Where(p => p.CategoriasProductos.Any(x => CategoriasProductos.Contains(x)));
            return promos;
        }      

        public async Task<IEnumerable<Promocion>> GetPromocionesVigentes(string medioDePago, string Banco, string categoriaProducto,DateTime Fecha)
        {
            var promo = await GetPromociones(x =>(x.FechaInicio >= DateTime.Now.Date && x.FechaFin <= Fecha) && x.Activo == true);
            var promos = promo.Where(p => p.MediosDePago.Contains(medioDePago));
            promos = promos.Where(p => p.Bancos.Contains(Banco));
            promos = promos.Where(p => p.CategoriasProductos.Contains(categoriaProducto));
            return promos;
        }

        public Task InsertarPromocion(Promocion promocion)
        {
            _promociones.Add(promocion);
            return CompletedTask;
        }
    }
}
