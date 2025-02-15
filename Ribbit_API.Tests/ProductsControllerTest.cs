using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Ribbit_API.Tests
{

    public class ProductsControllerTest : IClassFixture<WebApplicationFactory<Ribbit_API.Controllers.ProductsController>>
    {
        private readonly HttpClient _httpClient;

        public ProductsControllerTest(WebApplicationFactory<Ribbit_API.Controllers.ProductsController> factory)
        {
            this._httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ReturnOK()
        {
            var response = await this._httpClient.GetAsync("/api/products");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetProductById_ReturnOK(int id)
        {
            var response = await this._httpClient.GetAsync($"/api/products/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_ReturnOK()
        {
            var product = new
            {
                Nombre = "Converse",
                Descripcion = "Converse All Star",
                Precio = 1099.89m,
                FechaCreacion = DateTime.Now,
                Stock = 5
            };

            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await this._httpClient.PostAsync("/api/products/create",content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal("Producto creado con exito.", responseBody);
        }

        [Fact]
        public async Task AddProducto_ReturnCampoRequerido()
        {
            var product = new
            {
                Descripcion = "Converse All Star",
                Precio = 1099.89m,
                FechaCreacion = DateTime.Now,
                Stock = 5
            };

            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await this._httpClient.PostAsync("/api/products/create", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("campo es requerido", responseBody);
        }
    }
}
