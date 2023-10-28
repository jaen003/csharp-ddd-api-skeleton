using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class Uuid : NonEmptyString
{
    private const string PATTERN =
        @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-4[0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$";

    public Uuid(string value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidUuid(value);
        }
    }

    private bool IsValid()
    {
        return Matches(PATTERN);
    }
}
