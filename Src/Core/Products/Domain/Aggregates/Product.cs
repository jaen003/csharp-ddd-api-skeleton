using Src.Core.Products.Domain.Events;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Aggregates;

public class Product : AggregateRoot
{
    private readonly Uuid id;
    private NonEmptyString name;
    private NonNegativeInt price;
    private NonEmptyString description;
    private ProductStatus status;
    private readonly Uuid restaurantId;

    public string Id => id.Value;
    public string Name => name.Value;
    public int Price => price.Value;
    public string Description => description.Value;
    public short Status => status.Value;
    public string RestaurantId => restaurantId.Value;

    public Product(
        string id,
        string name,
        int price,
        string description,
        short status,
        string restaurantId
    )
    {
        this.id = new Uuid(id);
        this.name = new NonEmptyString(name);
        this.price = new NonNegativeInt(price);
        this.description = new NonEmptyString(description);
        this.status = new ProductStatus(status);
        this.restaurantId = new Uuid(restaurantId);
    }

    private Product(
        Uuid productId,
        NonEmptyString productName,
        NonNegativeInt productPrice,
        NonEmptyString productDescription,
        ProductStatus productStatus,
        Uuid productRestaurantId
    )
    {
        id = productId;
        name = productName;
        price = productPrice;
        description = productDescription;
        status = productStatus;
        restaurantId = productRestaurantId;
    }

    public static Product Create(
        string id,
        string name,
        int price,
        string description,
        string restaurantId
    )
    {
        List<CustomException> exceptions = new();
        Uuid? productId = null;
        NonEmptyString? productName = null;
        NonNegativeInt? productPrice = null;
        NonEmptyString? productDescription = null;
        Uuid? productRestaurantId = null;
        try
        {
            productId = new Uuid(id);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
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
        try
        {
            productRestaurantId = new Uuid(restaurantId);
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
                productId!,
                productName!,
                productPrice!,
                productDescription!,
                ProductStatus.CreateActived(),
                productRestaurantId!
            );
        product.RecordEvent(new ProductCreated(id, name, price, description));
        return product;
    }

    public void ChangePrice(int newPrice)
    {
        price = new NonNegativeInt(newPrice);
        RecordEvent(new ProductPriceChanged(id.Value, price.Value));
    }

    public void Delete()
    {
        status = ProductStatus.CreateDeleted();
        RecordEvent(new ProductDeleted(id.Value));
    }

    public void ChangeDescription(string newDescription)
    {
        description = new NonEmptyString(newDescription);
        RecordEvent(new ProductDescriptionChanged(id.Value, description.Value));
    }

    public void Rename(string newName)
    {
        name = new NonEmptyString(newName);
        RecordEvent(new ProductRenamed(id.Value, name.Value));
    }
}
