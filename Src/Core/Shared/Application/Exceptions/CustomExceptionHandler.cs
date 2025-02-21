using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Application.Exceptions;

public class CustomExceptionHandler
{
    private readonly ILogger logger;

    public CustomExceptionHandler(ILogger logger)
    {
        this.logger = logger;
    }

    public void Handle(CustomException exception)
    {
        if (exception.IsWarning())
        {
            logger.Warning(exception.Message);
        }
        else if (exception.IsError())
        {
            logger.Error(exception.Message);
        }
        else
        {
            logger.Critical(exception.Message);
        }
    }
}
