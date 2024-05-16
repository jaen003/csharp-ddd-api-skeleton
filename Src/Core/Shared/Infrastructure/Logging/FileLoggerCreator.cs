using Serilog;
using ILogger = Src.Core.Shared.Domain.Logging.ILogger;
using ISerilogLogger = Serilog.ILogger;

namespace Src.Core.Shared.Infrastructure.Logging;

public class FileLoggerCreator : LoggerCreator
{
    private readonly string logFilePath;

    public FileLoggerCreator()
    {
        logFilePath = GenerateLogFilePath();
    }

    public override ILogger Create()
    {
        ISerilogLogger serilogLogger = new LoggerConfiguration().MinimumLevel
            .ControlledBy(loggingLevelSwitch)
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        return new FileLogger(serilogLogger);
    }

    private static string GenerateLogFilePath()
    {
        string logsPath = Environment.GetEnvironmentVariable("LOGS_PATH")!;
        string logFileName = Environment.GetEnvironmentVariable("LOG_FILE_NAME")!;
        return $"{logsPath}/{logFileName}.log";
    }
}
