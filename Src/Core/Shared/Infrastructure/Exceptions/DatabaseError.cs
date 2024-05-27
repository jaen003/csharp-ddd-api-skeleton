namespace Src.Core.Shared.Infrastructure.Exceptions;

public class DatabaseError : InternalError
{
    public DatabaseError(string message)
        : base($"An internal database error occurred during the request: {message}.") { }
}
