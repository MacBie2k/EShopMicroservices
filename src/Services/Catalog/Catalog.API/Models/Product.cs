namespace Catalog.API.Models;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public List<string> Categories { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}