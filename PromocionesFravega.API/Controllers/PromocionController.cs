using PromocionesFravega.Core.Entities;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PromocionesFravega.Core.Interfaces;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.API.Responses;
using PromocionesFravega.Core.QueryFilters;

namespace PromocionesFravega.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocionController : ControllerBase
    {
        private readonly IPromocionService _promocionService;
        private readonly IPromocionRepository _repository;

        public PromocionController(IPromocionService promocionService)
        {
            _promocionService = promocionService ?? throw new ArgumentNullException(nameof(promocionService));            
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Promocion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Promocion>>> GetPromociones()
        {
            var promos = await _promocionService.GetPromociones();
            var res = new ApiResponse<IEnumerable<Promocion>>(promos);
            return Ok(res);
        }

        [HttpGet("GetPromocionesVigentes")]
        [ProducesResponseType(typeof(IEnumerable<Promocion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Promocion>>> GetPromocionesVigentes()
        {
            var promos = await _promocionService.GetPromocionesVigentes();
            var res = new ApiResponse<IEnumerable<Promocion>>(promos);
            return Ok(res);
        }

        
        [HttpGet("GetPromocionesVigentesPorFecha")]
        [ProducesResponseType(typeof(IEnumerable<Promocion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Promocion>>> GetPromocionesVigentesPorFecha([FromQuery] DateTime Fecha)
        {
            var promos = await _promocionService.GetPromocionesVigentes(Fecha);
            var res = new ApiResponse<IEnumerable<Promocion>>(promos);
            return Ok(res);
        }

        [HttpGet("GetPromocionesVigentesPorVenta")]
        [ProducesResponseType(typeof(IEnumerable<PromocionVigenteDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PromocionVigenteDto>>> GetPromocionesVigentesPorVenta(PromocionesPorVentaQueryFilter filter)
        {
            var promos = await _promocionService.GetPromocionesVigentes(filter.MedioDePago,filter.MedioDePago,filter.CategoriaProducto);
            var res = new ApiResponse<IEnumerable<PromocionVigenteDto>>(promos);
            return Ok(res);
        }

        [HttpGet("GetPromocion")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Promocion>> GetPromocionById(Guid id)
        {
            var promo = await _promocionService.GetPromocion(id);
            var res = new ApiResponse<Promocion>(promo);
            return Ok(res);
        }

        //[Route("[action]/{category}", Name = "GetProductByCategory")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        //{
        //    var products = await _repository.GetProductByCategory(category);
        //    return Ok(products);
        //}

        //[Route("[action]/{name}", Name = "GetProductByName")]
        //[HttpGet]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        //{
        //    var items = await _repository.GetProductByName(name);
        //    if (items == null)
        //    {
        //        _logger.LogError($"Products with name: {name} not found.");
        //        return NotFound();
        //    }
        //    return Ok(items);
        //}

        [HttpPost]
        //[ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> InsertarPromocion(PromocionDto promocion)
        {
            var Id = await _promocionService.CrearPromocion(promocion);
            var res = new ApiResponse<Guid>(Id);
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarPromocion([FromBody] PromocionUpdateDto promocion)
        {
            var promo = await _promocionService.ActualizarPromocion(promocion);
            var res = new ApiResponse<Guid>(promo);
            return Ok(res);            
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarPromocionVigencia([FromBody] PromocionVigenciaUpdateDto promocion)
        {
            var promo = await _promocionService.ActualizarPromocion(promocion);
            var res = new ApiResponse<Guid>(promo);
            return Ok(res);
        }

        [HttpDelete("{id:length(24)}", Name = "EliminarPromocion")]        
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EliminarPromocionById(Guid id)
        {
            var promo = await _promocionService.EliminarPromocion(id);
            var res = new ApiResponse<Guid>(promo);
            return Ok(res);
        }
    }
}
