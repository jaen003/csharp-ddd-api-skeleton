namespace Src.Core.Shared.Infrastructure.Exceptions;

public class EventBusError : InternalError
{
    public EventBusError(string message)
        : base($"An internal event bus error occurred during the request: {message}.") { }
}
