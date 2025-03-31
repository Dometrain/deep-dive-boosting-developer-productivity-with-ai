namespace OrderManager.API.Models;

public class OrderLineDto
{
    public int Id { get; set; }
    public string? Details { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
}
