using Microsoft.AspNetCore.Mvc;
using Ribbit_API.Data.Services;
using Ribbit_API.ExceptionHandling;
using Ribbit_API.Models;

namespace Ribbit_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService service)
        {
            _productsService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await _productsService.GetAllAsync();
            return Ok(products);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("/api/[controller]/create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid) { return BadRequest("Producto invalido"); }
            try
            {
                await _productsService.AddAsync(product);
                return Ok("Producto creado con exito.");
            }
            catch(Exception ex) { return StatusCode(500, "Servicio no disponible"); }
        }

        [HttpPut("/api/[controller]/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid) { return BadRequest("Producto invalido"); }
            try
            {
                await _productsService.UpdateProduct(id, product);
                return Ok();
            }
            catch (ProductNotFoundException nfEx) { return BadRequest(nfEx.Message); }
            catch (ProductNotMatchException nmEx) { return BadRequest(nmEx.Message); }
            catch (Exception ex) { return StatusCode(500, "Servicio no disponible"); }
        }

        [HttpDelete("/api/[controller]/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productsService.DeleteAsync(id);
                return Ok();
            }
            catch (ProductNotFoundException nfEx) { return BadRequest(nfEx.Message); }
            catch (Exception ex) { return StatusCode(500, "Servicio no disponible"); }
        }
    }
}
