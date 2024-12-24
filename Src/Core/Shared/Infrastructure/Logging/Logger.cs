using Src.Core.Shared.Application.Logging;
using ISerilogLogger = Serilog.ILogger;

namespace Src.Core.Shared.Infrastructure.Logging;

internal abstract class Logger : ILogger
{
    private readonly ISerilogLogger serilogLogger;

    protected Logger(ISerilogLogger serilogLogger)
    {
        this.serilogLogger = serilogLogger;
    }

    public void Critical(string message)
    {
        serilogLogger.Fatal(message);
    }

    public void Debug(string message)
    {
        serilogLogger.Debug(message);
    }

    public void Error(string message)
    {
        serilogLogger.Error(message);
    }

    public void Information(string message)
    {
        serilogLogger.Information(message);
    }

    public void Warning(string message)
    {
        serilogLogger.Warning(message);
    }
}
