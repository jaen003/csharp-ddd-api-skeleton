using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public record Email : NonEmptyString
{
    private const string Pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
    private const string ValueObjectName = "email";

    public Email(string value, string aggregateName)
        : base(value, ValueObjectName, aggregateName)
    {
        if (!IsValid())
        {
            throw new InvalidEmailFormatException(value, aggregateName);
        }
    }

    private bool IsValid()
    {
        return Matches(Pattern);
    }
}
