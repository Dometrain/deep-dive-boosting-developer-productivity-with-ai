using System.ComponentModel.DataAnnotations;

namespace OrderManager.API.Entities;

public class Vendor
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public required string Email { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
