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
        if (exception.IsDebug())
        {
            logger.Debug(exception.Message);
        }
        else if (exception.IsInformation())
        {
            logger.Information(exception.Message);
        }
        else if (exception.IsWarning())
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
