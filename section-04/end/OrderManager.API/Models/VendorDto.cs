namespace OrderManager.API.Models;

public class VendorDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public required string Email { get; set; }
}
