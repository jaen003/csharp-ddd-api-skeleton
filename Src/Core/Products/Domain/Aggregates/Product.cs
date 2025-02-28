using Src.Core.Products.Domain.Events;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Aggregates;

public class Product : AggregateRoot
{
    public Guid Id { get; }
    private ProductName name;
    private ProductPrice price;
    private ProductDescription description;
    private ProductStatus status;
    public Guid RestaurantId { get; }

    public string Name => name.Value;
    public int Price => price.Value;
    public string Description => description.Value;
    public short Status => status.Value;

    public Product(
        Guid id,
        string name,
        int price,
        string description,
        short status,
        Guid restaurantId
    )
    {
        Id = id;
        this.name = new ProductName(name);
        this.price = new ProductPrice(price);
        this.description = new ProductDescription(description);
        this.status = new ProductStatus(status);
        RestaurantId = restaurantId;
    }

    private Product(
        Guid productId,
        ProductName productName,
        ProductPrice productPrice,
        ProductDescription productDescription,
        ProductStatus productStatus,
        Guid restaurantId
    )
    {
        Id = productId;
        name = productName;
        price = productPrice;
        description = productDescription;
        status = productStatus;
        RestaurantId = restaurantId;
    }

    public static Product Create(
        Guid id,
        string name,
        int price,
        string description,
        Guid restaurantId
    )
    {
        List<CustomException> exceptions = new();
        ProductName? productName = null;
        ProductPrice? productPrice = null;
        ProductDescription? productDescription = null;
        try
        {
            productName = new ProductName(name);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        try
        {
            productPrice = new ProductPrice(price);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        try
        {
            productDescription = new ProductDescription(description);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        if (exceptions.Count > 0)
        {
            throw new MultipleCustomException(exceptions);
        }
        Product product =
            new(
                id,
                productName!,
                productPrice!,
                productDescription!,
                ProductStatus.CreateActive(),
                restaurantId
            );
        product.RecordEvent(new ProductCreatedDomainEvent(id, name, price, description));
        return product;
    }

    public void ChangePrice(int newPrice)
    {
        price = new ProductPrice(newPrice);
        RecordEvent(new ProductPriceChangedDomainEvent(Id, price.Value));
    }

    public void Delete()
    {
        status = ProductStatus.CreateDeleted();
        RecordEvent(new ProductDeletedDomainEvent(Id));
    }

    public void ChangeDescription(string newDescription)
    {
        description = new ProductDescription(newDescription);
        RecordEvent(new ProductDescriptionChangedDomainEvent(Id, description.Value));
    }

    public void Rename(string newName)
    {
        name = new ProductName(newName);
        RecordEvent(new ProductRenamedDomainEvent(Id, name.Value));
    }
}
