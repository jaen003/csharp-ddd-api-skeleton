namespace Src.Core.Products.Application.Dtos;

public record ProductDto
{
    public string Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }

    public ProductDto(string id, string name, int price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}
