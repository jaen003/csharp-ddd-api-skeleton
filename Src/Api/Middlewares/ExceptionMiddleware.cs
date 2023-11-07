using Src.Core.Shared.Domain.Exceptions;
using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;

namespace Src.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ApplicationExceptionHandler exceptionHandler;

    public ExceptionMiddleware(RequestDelegate next, ApplicationExceptionHandler exceptionHandler)
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
        catch (ApplicationException exception)
        {
            await HandleApplicationException(context, exception);
        }
        catch (MultipleApplicationException multipleException)
        {
            await HandleMultipleApplicationException(context, multipleException);
        }
    }

    private async Task HandleApplicationException(
        HttpContext context,
        ApplicationException exception
    )
    {
        exceptionHandler.Handle(exception);
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(
            new Dictionary<string, object> { { "code", exception.Code } }
        );
    }

    private async Task HandleMultipleApplicationException(
        HttpContext context,
        MultipleApplicationException multipleException
    )
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        List<Dictionary<string, object>> response = new();
        foreach (ApplicationException exception in multipleException.Exceptions)
        {
            exceptionHandler.Handle(exception);
            response.Add(new Dictionary<string, object> { { "code", exception.Code } });
        }
        await context.Response.WriteAsJsonAsync(response);
    }
}
