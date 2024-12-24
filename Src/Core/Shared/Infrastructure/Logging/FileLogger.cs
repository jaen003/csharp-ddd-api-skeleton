using Serilog;

namespace Src.Core.Shared.Infrastructure.Logging;

internal class FileLogger : Logger
{
    public FileLogger(ILogger logger)
        : base(logger) { }
}
