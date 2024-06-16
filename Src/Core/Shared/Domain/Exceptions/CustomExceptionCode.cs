namespace Src.Core.Shared.Domain.Exceptions;

public enum CustomExceptionCode : int
{
    UnexpectedNegativeNumber = 1,
    UnexpectedEmptyString = 2,
    InvalidEmail = 3,
    InvalidPaginationLimit = 4,
    InvalidSortingType = 5,
    UnexpectedNullSortingField = 6,
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
    InvalidProductStatus = 203
}
