namespace Src.Core.Products.Application.DTOs;

public record ProductResponseData
{
    public Guid Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }

    public ProductResponseData(Guid id, string name, int price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}
