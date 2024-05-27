using Serilog;
using ILogger = Src.Core.Shared.Application.Logging.ILogger;
using ISerilogLogger = Serilog.ILogger;

namespace Src.Core.Shared.Infrastructure.Logging;

public class ConsoleLoggerCreator : LoggerCreator
{
    public override ILogger Create()
    {
        ISerilogLogger serilogLogger = new LoggerConfiguration().MinimumLevel
            .ControlledBy(loggingLevelSwitch)
            .WriteTo.Console()
            .CreateLogger();
        return new ConsoleLogger(serilogLogger);
    }
}
