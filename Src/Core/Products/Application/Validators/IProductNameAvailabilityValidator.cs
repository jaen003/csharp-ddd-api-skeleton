namespace Src.Core.Products.Application.Validators;

public interface IProductNameAvailabilityValidator
{
    Task Validate(string productName, Guid restaurantId);
}
