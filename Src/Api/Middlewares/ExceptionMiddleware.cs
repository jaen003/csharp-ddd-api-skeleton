using Src.Core.Shared.Domain.Exceptions;

namespace Src.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly DomainExceptionHandler exceptionHandler;

    public ExceptionMiddleware(RequestDelegate next, DomainExceptionHandler exceptionHandler)
    {
        this.next = next;
        this.exceptionHandler = exceptionHandler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException exception)
        {
            exceptionHandler.Handle(exception);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(
                new Dictionary<string, object> { { "code", exception.Code } }
            );
        }
    }
}
