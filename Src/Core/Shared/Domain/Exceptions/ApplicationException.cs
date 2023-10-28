namespace Src.Core.Shared.Domain.Exceptions;

public abstract class ApplicationException : Exception
{
    protected const int CRITICAL = 1;
    protected const int ERROR = 2;
    protected const int WARNING = 3;
    protected const int INFORMATION = 4;
    protected const int DEBUG = 5;

    public int Code { get; }
    private readonly int type;

    protected ApplicationException(int code, int type, string message)
        : base(message)
    {
        Code = code;
        this.type = type;
    }

    public bool IsCritical()
    {
        return type == CRITICAL;
    }

    public bool IsError()
    {
        return type == ERROR;
    }

    public bool IsWarning()
    {
        return type == WARNING;
    }

    public bool IsInformation()
    {
        return type == INFORMATION;
    }

    public bool IsDebug()
    {
        return type == DEBUG;
    }
}
