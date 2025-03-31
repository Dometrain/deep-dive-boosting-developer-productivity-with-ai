using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
