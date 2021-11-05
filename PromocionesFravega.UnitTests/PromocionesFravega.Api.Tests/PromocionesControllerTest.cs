using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PromocionesFravega.Core.Services;
using PromocionesFravega.Core.Interfaces;
using PromocionesFravega.API.Controllers;
using PromocionesFravega.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;
using PromocionesFravega.API.Responses;
using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.QueryFilters;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Exceptions;

namespace PromocionesFravega.UnitTests.PromocionesFravega.Api.Tests
{
    public class PromocionesControllerTest
    {

        private readonly IMapper _mapper;
        private readonly IPromocionService _promocionService;
        private readonly PromocionController _controller;

        public PromocionesControllerTest()
        {
            IPromocionRepository promocionRepository = new PromocionRepositoryMock();
            var mapperconfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(mapperconfig);
            _promocionService = new PromocionService(promocionRepository,_mapper);
            _controller = new PromocionController(_promocionService);
        }

        //Se decide testear los controladores directamente y no unidades funcionales mas pequeñas para realizar una muestra 
        //general del testeo del proyecto completo.

        //Get
        [Fact]
        public async Task Get_AlInvocar_RetornaOkResultAsync()
        {
            //Arrange
            Guid id = new Guid("219eaa64-25a4-41d5-9509-cd76d0138b42");
            // Act
            var okResult = await _controller.GetPromocionById(id);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task Get_AlInvocar_RetornaTodasLasPromociones()
        {
            var okResult = await _controller.GetPromociones() as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<Promocion>>>(okResult.Value);
            var lista = (IEnumerable<Promocion>)items.Data;
            Assert.Equal(5, lista.Count());
        }

        [Fact]
        public async Task Get_AlInvocar_RetornaTodasLasPromocionesVigentes()
        {
            var okResult = await _controller.GetPromocionesVigentes() as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<Promocion>>>(okResult.Value);
            var lista = (IEnumerable<Promocion>)items.Data;
            Assert.Single(lista);
        }

        [Theory]
        [InlineData("2021-12-08", "219eaa64-25a4-41d5-9509-cd76d0138b42")]
        [InlineData("2021-07-15", "4b46e3b6-89cc-42bc-bdd7-6fcae2f0af93")]
        public async Task Get_AlInvocar_RetornaPromocionesVigentesParaFecha(DateTime Fecha, string expected)
        {
            var okResult = await _controller.GetPromocionesVigentesPorFecha(Fecha) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<Promocion>>>(okResult.Value);
            var lista = (List<Promocion>)items.Data;
            var promocion = lista.First();

            Assert.Equal(expected, promocion.Id.ToString());
        }

        [Theory]
        [InlineData("2021-09-15", "f66ddfd1-dc16-4b8d-ab69-79dc0925231e")]
        [InlineData("2021-09-29", "2c3269af-e89c-4c97-ab40-964e5f222b08")]
        public async Task Get_AlInvocar_RetornaPromocionesVigentesParaFechaDentroDelMismoRango(DateTime Fecha, string expected)
        {
            var okResult = await _controller.GetPromocionesVigentesPorFecha(Fecha) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<Promocion>>>(okResult.Value);
            var lista = (List<Promocion>)items.Data;
            var ListaGuids = lista.Select(x => x.Id.ToString());
            Assert.Contains(expected,ListaGuids);
        }

        [Fact]
        public async Task Get_AlInvocar_RetornaTodasLasPromocionesVigentesParaVenta()
        {
            PromocionesPorVentaQueryFilter filter = new PromocionesPorVentaQueryFilter();
            filter.MedioDePago = "GIFT_CARD";
            filter.Banco = "Macro";
            filter.CategoriaProducto = "Tecnologia";

            var okResult = await _controller.GetPromocionesVigentesPorVenta(filter) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<PromocionVigenteDto>>>(okResult.Value);
            var lista = (IEnumerable<PromocionVigenteDto>)items.Data;
            
            Assert.Single(lista);
            Assert.Equal("219eaa64-25a4-41d5-9509-cd76d0138b42", lista.First().Id.ToString());
        }

        [Fact]
        public async Task Get_AlInvocar_RetornaTodasLasPromocionesVigentesParaVentaConDistintaCategoria()
        {
            PromocionesPorVentaQueryFilter filter = new PromocionesPorVentaQueryFilter();
            filter.MedioDePago = "GIFT_CARD";
            filter.Banco = "Macro";
            filter.CategoriaProducto = "Celulares";

            var okResult = await _controller.GetPromocionesVigentesPorVenta(filter) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<PromocionVigenteDto>>>(okResult.Value);
            var lista = (IEnumerable<PromocionVigenteDto>)items.Data;

            Assert.Single(lista);
            Assert.Equal("219eaa64-25a4-41d5-9509-cd76d0138b42", lista.First().Id.ToString());
        }

        [Fact]
        public async Task Get_AlInvocar_RetornaTodasLasPromocionesVigentesParaVenta_CategoriaNoIncluida_RetornaListaVacia()
        {
            PromocionesPorVentaQueryFilter filter = new PromocionesPorVentaQueryFilter();
            filter.MedioDePago = "GIFT_CARD";
            filter.Banco = "Macro";
            filter.CategoriaProducto = "Jardin";

            var okResult = await _controller.GetPromocionesVigentesPorVenta(filter) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<IEnumerable<PromocionVigenteDto>>>(okResult.Value);
            var lista = (IEnumerable<PromocionVigenteDto>)items.Data;

            Assert.Empty(lista);            
        }

        [Fact]
        public async Task Get_AlInvocar_CreaUnaPromocion()
        {
            PromocionDto promo = new PromocionDto();
            promo.MediosDePago = new List<string> { "EFECTIVO" };
            promo.Bancos = new List<string> { "Galicia" };
            promo.CategoriasProductos = new List<string> { "Audio","Tecnologia" };
            promo.FechaInicio = DateTime.Parse("01/01/2022");
            promo.FechaFin = DateTime.Parse("31/01/2022");
            promo.MaximaCantidadDeCuotas = 12; 
            promo.ValorInteresCuotas = 1;            

            var okResult = await _controller.InsertarPromocion(promo) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<Guid>>(okResult.Value);
            var guid = (Guid)items.Data;

            var promos = await _promocionService.GetPromociones();
            Assert.Equal(6,promos.Count());
        }


        [Fact]
        public async Task Get_AlInvocar_IntentaCrearPromocion_ValidacionSuperposicionParaFecha()
        {
            PromocionDto promo = new PromocionDto();
            promo.MediosDePago = new List<string> { "GIFT_CARD" };
            promo.Bancos = new List<string> { "Macro" };
            promo.CategoriasProductos = new List<string> { "Audio", "Tecnologia" };
            promo.FechaInicio = DateTime.Parse("05/11/2021");
            promo.FechaFin = DateTime.Parse("31/01/2022");
            promo.MaximaCantidadDeCuotas = 12;
            promo.ValorInteresCuotas = 1;

            await Assert.ThrowsAsync<BusinessException>(() => _controller.InsertarPromocion(promo));
           
        }



        [Fact]
        public async Task Get_AlInvocar_ModificaUnaPromocion()
        {
            PromocionUpdateDto promo = new PromocionUpdateDto();
            promo.Id =new Guid("2c3269af-e89c-4c97-ab40-964e5f222b08");
            promo.MediosDePago = new List<string> { "EFECTIVO" };
            promo.Bancos = new List<string> { "BBVA" };
            promo.CategoriasProductos = new List<string> { "Audio", "Tecnologia" };
            promo.FechaInicio = DateTime.Parse("01/01/2022");
            promo.FechaFin = DateTime.Parse("31/01/2022");
            promo.MaximaCantidadDeCuotas = 12;
            promo.ValorInteresCuotas = 1;

            var okResult = await _controller.ActualizarPromocion(promo) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<Guid>>(okResult.Value);
            var guid = (Guid)items.Data;

            var promoUpd = await _promocionService.GetPromocion(guid);
            Assert.Contains("BBVA", promoUpd.Bancos);
        }

        [Fact]
        public async Task Get_AlInvocar_ModificaVigenciaPromocion()
        {
            PromocionVigenciaUpdateDto promo = new PromocionVigenciaUpdateDto();
            promo.Id = new Guid("2c3269af-e89c-4c97-ab40-964e5f222b08");
            promo.FechaInicio = DateTime.Parse("06/04/2021");
            promo.FechaFin = DateTime.Parse("18/07/2021");
            
            var okResult = await _controller.ActualizarPromocionVigencia(promo) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<Guid>>(okResult.Value);
            var guid = (Guid)items.Data;

            var promoUpd = await _promocionService.GetPromocion(guid);
            Assert.Equal(DateTime.Parse("06/04/2021"), promoUpd.FechaInicio.Value.Date);
            Assert.Equal(DateTime.Parse("18/07/2021"), promoUpd.FechaFin.Value.Date);
        }

        [Fact]
        public async Task Get_AlInvocar_EliminarPromocion()
        {
            PromocionVigenciaUpdateDto promo = new PromocionVigenciaUpdateDto();
            promo.Id = new Guid("2c3269af-e89c-4c97-ab40-964e5f222b08");
            
            var okResult = await _controller.EliminarPromocionById(promo.Id) as OkObjectResult;
            var items = Assert.IsType<ApiResponse<Guid>>(okResult.Value);
            var guid = (Guid)items.Data;

            var promoElim = await _promocionService.GetPromocion(guid);
            Assert.False(promoElim.Activo);            

        }

    }
}
