namespace Src.Core.Shared.Domain.Exceptions;

public abstract class CustomException : Exception
{
    private readonly CustomExceptionSeverityLevel severityLevel;
    private readonly CustomExceptionCode code;

    public int Code => (int)code;

    protected CustomException(
        CustomExceptionCode code,
        CustomExceptionSeverityLevel severityLevel,
        string message
    )
        : base(message)
    {
        this.code = code;
        this.severityLevel = severityLevel;
    }

    public bool IsCritical()
    {
        return severityLevel == CustomExceptionSeverityLevel.Critical;
    }

    public bool IsError()
    {
        return severityLevel == CustomExceptionSeverityLevel.Error;
    }

    public bool IsWarning()
    {
        return severityLevel == CustomExceptionSeverityLevel.Warning;
    }
}
