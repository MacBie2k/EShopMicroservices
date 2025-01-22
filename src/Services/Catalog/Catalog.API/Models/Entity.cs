namespace Catalog.API.Models;

public class Entity<T>
{
    public T Id { get; set; } = default(T)!;
}