using PromocionesFravega.Core.Entities;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PromocionesFravega.Core.Interfaces;

namespace PromocionesFravega.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocionController : ControllerBase
    {
        private readonly IPromocionRepository _repository;

        public PromocionController(IPromocionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));            
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Promocion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Promocion>>> GetProducts()
        {
            var products = await _repository.GetPromociones();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetPromocion")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Promocion>> GetPromocionById(string id)
        {
            var product = await _repository.GetPromocion(id);

            if (product == null)
            {
                //_logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
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
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Promocion>> InsertarPromocion([FromBody] Promocion promocion)
        {
            await _repository.InsertarPromocion(promocion);

            return CreatedAtRoute("GetPromocion", new { id = promocion.Id }, promocion);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarPromocion([FromBody] Promocion promocion)
        {
            return Ok(await _repository.ActualizarPromocion(promocion));
        }

        [HttpDelete("{id:length(24)}", Name = "EliminarPromocion")]        
        [ProducesResponseType(typeof(Promocion), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EliminarPromocionById(string id)
        {
            return Ok(await _repository.EliminarPromocion(id));
        }
    }
}
