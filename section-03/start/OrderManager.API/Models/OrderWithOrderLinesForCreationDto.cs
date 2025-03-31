using System.ComponentModel.DataAnnotations;

namespace OrderManager.API.Models;

public class OrderWithOrderLinesForCreationDto
{
    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? ShippingDate { get; set; }

    [Required]
    public decimal OrderTotal { get; set; }

    public List<OrderLineForCreationDto> OrderLines { get; set; } = [];
}
