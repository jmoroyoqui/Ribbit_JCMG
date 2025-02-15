
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ribbit_API.Models;
using System.Text;

namespace Ribbit_JCMG.API
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string API = "https://localhost:7084/api";

        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{API}/products");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(jsonString);
            }
            return new List<Product>();
        }

        public async Task<Product> GetProductoIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{API}/products/{id}");

            if (response.IsSuccessStatusCode) 
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(jsonString);
            }
            return new Product();
        }

        public async Task<ObjectResult> AddProductAsync(Product product) 
        {
            var json = System.Text.Json.JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{API}/products/create", content);

            if (response.IsSuccessStatusCode) 
            {
                return new ObjectResult(new { Success = true, Message = "" })
                {
                    StatusCode = (int) response.StatusCode
                };
            }
            else
            {
                return new ObjectResult(new { Success = false, Message = response.Content.ReadAsStringAsync() })
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }

        public async Task<ObjectResult> UpdateProductAsync(int id, Product product)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{API}/products/update/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return new ObjectResult(new
                {
                    Success = true,
                    Message = ""
                })
                {
                    StatusCode = (int)response.StatusCode
                };
            }
            else
            {
                return new ObjectResult(new
                {
                    Success = false,
                    Message = response.Content.ReadAsStringAsync()
                })
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }

        public async Task<ObjectResult> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{API}/products/delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new ObjectResult(new
                {
                    Success = true,
                    Message = ""
                })
                {
                    StatusCode = (int)response.StatusCode
                };
            }
            else
            {
                return new ObjectResult(new
                {
                    Success = false,
                    Message = response.Content.ReadAsStringAsync()
                })
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}
