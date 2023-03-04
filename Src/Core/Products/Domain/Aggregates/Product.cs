using Src.Core.Products.Domain.Events;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Aggregates;

namespace Src.Core.Products.Domain.Aggregates;

public class Product : AggregateRoot
{
    public ProductId Id { get; }
    public ProductName Name { get; private set; }
    public ProductPrice Price { get; private set; }
    public ProductDescription Description { get; private set; }
    public ProductStatus Status { get; private set; }
    public RestaurantId RestaurantId { get; }

    public Product(
        ProductId id,
        ProductName name,
        ProductPrice price,
        ProductDescription description,
        ProductStatus status,
        RestaurantId restaurantId
    )
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Status = status;
        RestaurantId = restaurantId;
    }

    public static Product Create(
        ProductId id,
        ProductName name,
        ProductPrice price,
        ProductDescription description,
        RestaurantId restaurantId
    )
    {
        Product product =
            new(id, name, price, description, ProductStatus.CreateActived(), restaurantId);
        product.RecordEvent(
            new ProductCreated(id.Value, name.Value, price.Value, description.Value)
        );
        return product;
    }

    public void ChangePrice(ProductPrice newPrice)
    {
        Price = newPrice;
        RecordEvent(new ProductPriceChanged(Id.Value, Price.Value));
    }

    public void Delete()
    {
        Status = ProductStatus.CreateDeleted();
        RecordEvent(new ProductDeleted(Id.Value));
    }
}
