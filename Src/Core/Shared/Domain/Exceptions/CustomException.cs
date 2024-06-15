namespace Src.Core.Shared.Domain.Exceptions;

public abstract class CustomException : Exception
{
    protected enum SeverityLevel : short
    {
        Critical,
        Error,
        Warning,
        Information,
        Debug
    }

    private readonly SeverityLevel severityLevel;
    private readonly CustomExceptionCode code;

    public int Code => (int)code;

    protected CustomException(CustomExceptionCode code, SeverityLevel severityLevel, string message)
        : base(message)
    {
        this.code = code;
        this.severityLevel = severityLevel;
    }

    public bool IsCritical()
    {
        return severityLevel == SeverityLevel.Critical;
    }

    public bool IsError()
    {
        return severityLevel == SeverityLevel.Error;
    }

    public bool IsWarning()
    {
        return severityLevel == SeverityLevel.Warning;
    }

    public bool IsInformation()
    {
        return severityLevel == SeverityLevel.Information;
    }

    public bool IsDebug()
    {
        return severityLevel == SeverityLevel.Debug;
    }
}
