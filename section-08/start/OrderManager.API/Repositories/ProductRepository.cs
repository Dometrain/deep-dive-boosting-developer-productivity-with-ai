using Microsoft.EntityFrameworkCore;
using OrderManager.API.DbContexts;
using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(OrderManagerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
}
