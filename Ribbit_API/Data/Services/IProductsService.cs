using Ribbit_API.Data.Base;
using Ribbit_API.Models;

namespace Ribbit_API.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task UpdateProduct(int id, Product product);
    }
}
