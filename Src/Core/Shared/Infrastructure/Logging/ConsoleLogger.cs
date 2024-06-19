using Serilog;

namespace Src.Core.Shared.Infrastructure.Logging;

public class ConsoleLogger : Logger
{
    public ConsoleLogger(ILogger logger)
        : base(logger) { }
}
