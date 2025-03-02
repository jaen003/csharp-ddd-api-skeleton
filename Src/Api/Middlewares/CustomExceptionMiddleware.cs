using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Api.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly CustomExceptionHandler exceptionHandler;

    public CustomExceptionMiddleware(RequestDelegate next, CustomExceptionHandler exceptionHandler)
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
        catch (CustomException exception)
        {
            await HandleCustomException(context, exception);
        }
        catch (MultipleCustomException multipleException)
        {
            await HandleMultipleCustomException(context, multipleException);
        }
    }

    private async Task HandleCustomException(HttpContext context, CustomException exception)
    {
        exceptionHandler.Handle(exception);
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(
            new Dictionary<string, object> { { "code", exception.Code } }
        );
    }

    private async Task HandleMultipleCustomException(
        HttpContext context,
        MultipleCustomException multipleException
    )
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        List<Dictionary<string, object>> response = [];
        foreach (CustomException exception in multipleException.Exceptions)
        {
            exceptionHandler.Handle(exception);
            response.Add(new Dictionary<string, object> { { "code", exception.Code } });
        }
        await context.Response.WriteAsJsonAsync(response);
    }
}
