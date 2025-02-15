using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ribbit_API.Models;
using Ribbit_JCMG.API;
using System.Net;

namespace Ribbit_JCMG.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiService _apiService;

        public ProductsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _apiService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> GetProductos()
        {
            var productos = await _apiService.GetProductsAsync();
            return PartialView("_ProductosTablePartialView", productos);
        }



        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Product product)
        {
            product.FechaCreacion = DateTime.Now;

            var response = await _apiService.AddProductAsync(product);


            if (response.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = response.Value;
            return View(product);
        }


        public async Task<IActionResult> Detalles(int id)
        {
            var response = await _apiService.GetProductoIdAsync(id);

            if(response == null)
            {
                return View("NotFound");
            }
            return View(response);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var response = await _apiService.GetProductoIdAsync(id);
            if(response == null)
            {
                return View("NotFound");
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Product product)
        {
            var response = await _apiService.UpdateProductAsync(id, product);
            if (response.StatusCode == 200)
            {
                return RedirectToAction(nameof(Detalles), new { id = id });
            }
            return View(response);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await _apiService.GetProductoIdAsync(id);
            if (response == null)
            {
                return View("NotFound");
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminacion(int id)
        {
            var response = await _apiService.DeleteProductAsync(id);
            if (response.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(response);
        }
    }
}
