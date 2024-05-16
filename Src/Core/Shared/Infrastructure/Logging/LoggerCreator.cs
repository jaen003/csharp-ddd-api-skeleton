using Serilog.Core;
using Serilog.Events;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Shared.Infrastructure.Logging;

public abstract class LoggerCreator
{
    protected readonly LoggingLevelSwitch loggingLevelSwitch;

    protected LoggerCreator()
    {
        loggingLevelSwitch = GenerateLoggingLevelSwitch();
    }

    private static LoggingLevelSwitch GenerateLoggingLevelSwitch()
    {
        string loggerLevel = Environment.GetEnvironmentVariable("LOGGER_LEVEL")!;
        LogEventLevel logEventLevel = loggerLevel switch
        {
            "DEBUG" => LogEventLevel.Debug,
            "INFORMATION" => LogEventLevel.Information,
            "WARNING" => LogEventLevel.Warning,
            "ERROR" => LogEventLevel.Error,
            "CRITICAL" => LogEventLevel.Fatal,
            _ => LogEventLevel.Warning,
        };
        return new LoggingLevelSwitch(logEventLevel);
    }

    public abstract ILogger Create();
}
