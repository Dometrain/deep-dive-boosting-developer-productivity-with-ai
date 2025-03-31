using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Entities;
using OrderManager.API.Models;
using OrderManager.API.Repositories;

namespace OrderManager.API.Handlers;

public static class ProductsHandlers
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The collection of product DTOs.</returns>
    public static async Task<Ok<IEnumerable<ProductDto>>> GetProductsAsync(
        [FromServices] IProductRepository productRepository,
        [FromServices] IMapper mapper)
    {
        // Use the repository to get the products
        var productEntities = await productRepository.GetAllProductsAsync();

        // Map the entities to DTOs
        var productDtos = mapper.Map<IEnumerable<ProductDto>>(productEntities);

        // Return the mapped DTOs
        return TypedResults.Ok(productDtos);
    }
}
