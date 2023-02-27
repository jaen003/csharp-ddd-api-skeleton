using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonEmptyStringValueObject : StringValueObject
{
    public NonEmptyStringValueObject(string value)
        : base(value)
    {
        if (IsEmpty())
        {
            throw new EmptyStringException();
        }
    }
}
