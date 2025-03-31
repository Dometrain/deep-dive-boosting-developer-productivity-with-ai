using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.API.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; } = [];
    public ICollection<Vendor> Vendors { get; set; } = [];
}
