namespace OrderManager.API.Models;

public class OrderDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public decimal OrderTotal { get; set; }
    public List<OrderLineDto> OrderLines { get; set; } = [];
}
