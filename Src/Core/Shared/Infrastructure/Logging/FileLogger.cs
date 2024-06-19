using Serilog;

namespace Src.Core.Shared.Infrastructure.Logging;

public class FileLogger : Logger
{
    public FileLogger(ILogger logger)
        : base(logger) { }
}
