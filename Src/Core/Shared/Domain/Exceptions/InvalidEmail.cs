namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmail : ValidationError
{
    private const int CODE = 6;

    public InvalidEmail(string email)
        : base(CODE, $"The email '{email}' is invalid.") { }
}
