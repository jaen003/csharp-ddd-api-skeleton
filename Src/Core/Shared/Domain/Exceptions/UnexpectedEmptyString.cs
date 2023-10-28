namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedEmptyString : ValidationError
{
    private const int CODE = 5;

    public UnexpectedEmptyString()
        : base(CODE, "The string must not be empty.") { }
}
