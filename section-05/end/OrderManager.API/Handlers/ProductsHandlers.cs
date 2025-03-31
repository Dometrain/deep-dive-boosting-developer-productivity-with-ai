using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Models;
using OrderManager.API.Repositories;

namespace OrderManager.API.Handlers;

public static class ProductsHandlers
{
    private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("ProductsHandlers");

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The collection of product DTOs.</returns>
    public static async Task<IResult> GetProductsAsync(
        [FromServices] IProductRepository productRepository,
        [FromServices] IMapper mapper)
    {
        if (productRepository == null)
        {
            _logger.LogError("Product repository is null.");
            return Results.Problem("Internal server error.");
        }

        if (mapper == null)
        {
            _logger.LogError("Mapper is null.");
            return Results.Problem("Internal server error.");
        }

        try
        {
            // Use the repository to get the products
            var productEntities = await productRepository.GetAllProductsAsync();

            // Map the entities to DTOs
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(productEntities);

            // Return the mapped DTOs
            return Results.Ok(productDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving products.");
            return Results.Problem("An error occurred while retrieving products.");
        }
    }
}
