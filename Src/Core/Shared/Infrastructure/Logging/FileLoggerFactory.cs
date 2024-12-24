using Serilog;
using ILogger = Src.Core.Shared.Application.Logging.ILogger;
using ISerilogLogger = Serilog.ILogger;

namespace Src.Core.Shared.Infrastructure.Logging;

public class FileLoggerFactory : LoggerFactory
{
    private const string LOG_FILE_DIRECTORY = "logs";

    private readonly string logFilePath;

    public FileLoggerFactory()
    {
        logFilePath = GenerateLogFilePath();
    }

    public override ILogger Create()
    {
        ISerilogLogger serilogLogger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(loggingLevelSwitch)
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        return new FileLogger(serilogLogger);
    }

    private static string GenerateLogFilePath()
    {
        string logFileName = Environment.GetEnvironmentVariable("LOG_FILE_NAME")!;
        return $"{LOG_FILE_DIRECTORY}/{logFileName}.log";
    }
}
