using Src.Core.Products.Domain.Events;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Domain.Aggregates;

namespace Src.Core.Products.Domain.Aggregates;

public class Product : AggregateRoot
{
    private readonly Uuid id;
    private NonEmptyString name;
    private NonNegativeInt price;
    private NonEmptyString description;
    private ProductStatus status;
    private readonly Uuid restaurantId;

    public string Id
    {
        get { return id.Value; }
    }
    public string Name
    {
        get { return name.Value; }
    }
    public int Price
    {
        get { return price.Value; }
    }
    public string Description
    {
        get { return description.Value; }
    }
    public short Status
    {
        get { return status.Value; }
    }
    public string RestaurantId
    {
        get { return restaurantId.Value; }
    }

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

    public Product(
        string id,
        string name,
        int price,
        string description,
        ProductStatus status,
        string restaurantId
    )
    {
        this.id = new Uuid(id);
        this.name = new NonEmptyString(name);
        this.price = new NonNegativeInt(price);
        this.description = new NonEmptyString(description);
        this.status = status;
        this.restaurantId = new Uuid(restaurantId);
    }

    public static Product Create(
        string id,
        string name,
        int price,
        string description,
        string restaurantId
    )
    {
        Product product =
            new(id, name, price, description, ProductStatus.CreateActived(), restaurantId);
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
