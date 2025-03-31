using System.ComponentModel.DataAnnotations;

namespace OrderManager.API.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }     
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public decimal OrderTotal { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; } = [];
}
