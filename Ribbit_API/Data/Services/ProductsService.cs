using Microsoft.EntityFrameworkCore;
using Ribbit_API.Data.Base;
using Ribbit_API.DBContext;
using Ribbit_API.ExceptionHandling;
using Ribbit_API.Models;

namespace Ribbit_API.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        public ProductsService(AppDbContext context) : base(context)
        {
            
        }

        public async Task UpdateProduct(int id, Product product)
        {

            var productDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (productDb == null) throw new ProductNotFoundException(id);

            try
            {
                await base.UpdateAsync(id, product);
            }
            catch (Exception) { throw; }
        }
    }
}
