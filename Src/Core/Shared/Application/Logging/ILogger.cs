namespace Src.Core.Shared.Application.Logging;

public interface ILogger
{
    void Critical(string message);

    void Error(string message);

    void Warning(string message);

    void Information(string message);

    void Debug(string message);
}
