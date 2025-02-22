using Src.Core.Products.Domain.Events;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.Aggregates;

public class Product : AggregateRoot
{
    public Guid Id { get; }
    private NonEmptyString name;
    private NonNegativeInt price;
    private NonEmptyString description;
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
        this.name = new NonEmptyString(name);
        this.price = new NonNegativeInt(price);
        this.description = new NonEmptyString(description);
        this.status = new ProductStatus(status);
        RestaurantId = restaurantId;
    }

    private Product(
        Guid productId,
        NonEmptyString productName,
        NonNegativeInt productPrice,
        NonEmptyString productDescription,
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
        NonEmptyString? productName = null;
        NonNegativeInt? productPrice = null;
        NonEmptyString? productDescription = null;
        try
        {
            productName = new NonEmptyString(name);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        try
        {
            productPrice = new NonNegativeInt(price);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        try
        {
            productDescription = new NonEmptyString(description);
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
        price = new NonNegativeInt(newPrice);
        RecordEvent(new ProductPriceChangedDomainEvent(Id, price.Value));
    }

    public void Delete()
    {
        status = ProductStatus.CreateDeleted();
        RecordEvent(new ProductDeletedDomainEvent(Id));
    }

    public void ChangeDescription(string newDescription)
    {
        description = new NonEmptyString(newDescription);
        RecordEvent(new ProductDescriptionChangedDomainEvent(Id, description.Value));
    }

    public void Rename(string newName)
    {
        name = new NonEmptyString(newName);
        RecordEvent(new ProductRenamedDomainEvent(Id, name.Value));
    }
}
