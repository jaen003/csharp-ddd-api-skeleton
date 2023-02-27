using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Src.Core.Shared.Infrastructure.Logging;

public class ApplicationLoggerCreator
{
    private readonly string logFilePath;
    private readonly LoggingLevelSwitch loggingLevelSwitch;

    public ApplicationLoggerCreator()
    {
        logFilePath = GenerateLogFilePath();
        loggingLevelSwitch = GenerateLoggingLevelSwitch();
    }

    public ApplicationLogger Create()
    {
        Logger logger = new LoggerConfiguration().MinimumLevel
            .ControlledBy(loggingLevelSwitch)
            .WriteTo.Console()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        return new ApplicationLogger(logger);
    }

    private static string GenerateLogFilePath()
    {
        string logsPath = Environment.GetEnvironmentVariable("LOGS_PATH")!;
        string logFileName = Environment.GetEnvironmentVariable("LOG_FILE_NAME")!;
        return $"{logsPath}/{logFileName}.log";
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
}
