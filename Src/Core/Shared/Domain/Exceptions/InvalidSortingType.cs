namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidSortingType : ValidationError
{
    private const int CODE = 9;

    public InvalidSortingType(string type)
        : base(CODE, $"The sorting type '{type}' is invalid.") { }
}
