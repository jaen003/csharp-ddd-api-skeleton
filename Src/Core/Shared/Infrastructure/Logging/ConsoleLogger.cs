using Serilog;

namespace Src.Core.Shared.Infrastructure.Logging;

internal class ConsoleLogger : Logger
{
    public ConsoleLogger(ILogger logger)
        : base(logger) { }
}
