namespace Src.Core.Shared.Domain.Exceptions;

public class EventBusErrorException : InternalErrorException
{
    public EventBusErrorException(string message)
        : base($"An internal event bus error occurred during the request: {message}.") { }

    public EventBusErrorException() { }

    public EventBusErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public EventBusErrorException(int code, int type, string message)
        : base(code, type, message) { }
}
