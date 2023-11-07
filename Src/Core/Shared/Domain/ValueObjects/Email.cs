using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class Email : NonEmptyString
{
    private const string PATTERN = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";

    public Email(string value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidEmail(value);
        }
    }

    private bool IsValid()
    {
        return Matches(PATTERN);
    }
}
