namespace Src.Core.Products.Application.Services;

public interface IProductNameAvailabilityValidator
{
    Task Validate(string productName, Guid restaurantId);
}
