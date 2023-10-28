using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Shared.Domain.Exceptions;

public class ApplicationExceptionHandler
{
    private readonly ILogger logger;

    public ApplicationExceptionHandler(ILogger logger)
    {
        this.logger = logger;
    }

    public void Handle(ApplicationException exception)
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
