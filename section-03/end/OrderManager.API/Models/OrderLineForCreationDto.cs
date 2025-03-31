using System.ComponentModel.DataAnnotations;

namespace OrderManager.API.Models;

public class OrderLineForCreationDto
{
    [MaxLength(300)]
    public string? Details { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int ProductId { get; set; } 
}
