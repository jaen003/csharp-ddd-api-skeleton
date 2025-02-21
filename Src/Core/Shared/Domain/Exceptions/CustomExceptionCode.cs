namespace Src.Core.Shared.Domain.Exceptions;

public enum CustomExceptionCode : ushort
{
    NegativeNumberNotAllowed = 1,
    EmptyStringNotAllowed = 2,
    InvalidEmailFormat = 3,
    InvalidPaginationLimit = 4,
    InvalidPaginationSortingType = 5,
    NullPaginationSortingFieldNotAllowed = 6,
    InvalidUuid = 7,
    EventBusNotConfigured = 8,
    EventBusMessagePublishingFailed = 9,
    DomainEventConsumptionFailed = 10,
    InvalidDomainEventStructure = 11,
    DatabaseOperationFailed = 12,
    RestaurantNotFound = 101,
    InvalidRestaurantStatus = 102,
    ProductNameNotAvailable = 201,
    ProductNotFound = 202,
    InvalidProductStatus = 203,
}
