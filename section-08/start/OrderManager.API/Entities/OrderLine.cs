using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManager.API.Entities;

public class OrderLine
{
    [Key]
    public int Id { get; set; }
    public string? Details { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    
    // Foreign key to Product
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
    
    // Foreign key to Order
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;
}
