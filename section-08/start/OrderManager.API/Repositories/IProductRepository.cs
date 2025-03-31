using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
