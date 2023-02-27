using Serilog.Core;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Shared.Infrastructure.Logging;

public class ApplicationLogger : ILogger
{
    private readonly Logger logger;

    public ApplicationLogger(Logger logger)
    {
        this.logger = logger;
    }

    public void Critical(string message)
    {
        logger.Fatal(message);
    }

    public void Debug(string message)
    {
        logger.Debug(message);
    }

    public void Error(string message)
    {
        logger.Error(message);
    }

    public void Information(string message)
    {
        logger.Information(message);
    }

    public void Warning(string message)
    {
        logger.Warning(message);
    }
}
