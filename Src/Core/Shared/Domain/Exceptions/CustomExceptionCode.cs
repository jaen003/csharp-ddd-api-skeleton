namespace Src.Core.Shared.Domain.Exceptions;

public enum CustomExceptionCode : int
{
    UnexpectedNegativeInt = 1,
    UnexpectedEmptyString = 2,
    InvalidEmail = 3,
    InvalidPaginationLimit = 4,
    InvalidSortingType = 5,
    UnexpectedNullSortingField = 6,
    UnexpectedNegativeLong = 7,
    UnexpectedNegativeShort = 8,
    InvalidUuid = 9,
    EventBusNotConfigured = 10,
    EventBusMessagePublishingFailed = 11,
    DomainEventConsumptionFailed = 12,
    InvalidDomainEventStructure = 13,
    DatabaseOperationFailed = 14,
    RestaurantNotFound = 101,
    InvalidRestaurantStatus = 102,
    ProductNameNotAvailable = 201,
    ProductNotFound = 202,
    InvalidProductStatus = 203
}
