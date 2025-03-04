using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public record Email : NonEmptyString
{
    private const string PATTERN = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
    private const string VALUE_OBJECT_NAME = "email";

    public Email(string value, string aggregateName)
        : base(value, VALUE_OBJECT_NAME, aggregateName)
    {
        if (!IsValid())
        {
            throw new InvalidEmailFormatException(value, aggregateName);
        }
    }

    private bool IsValid()
    {
        return Matches(PATTERN);
    }
}
