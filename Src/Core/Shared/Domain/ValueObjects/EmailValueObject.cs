using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class EmailValueObject : NonEmptyStringValueObject
{
    private const string PATTERN = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";

    public EmailValueObject(string value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidEmailException();
        }
    }

    private bool IsValid()
    {
        return Matches(new StringValueObject(PATTERN));
    }
}
