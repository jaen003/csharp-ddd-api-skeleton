namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeLong : DomainException
{
    private const int CODE = 11;

    public UnexpectedNegativeLong(long nonNegativelong)
        : base(CODE, $"The long integer '{nonNegativelong}' must not be negative.") { }
}
