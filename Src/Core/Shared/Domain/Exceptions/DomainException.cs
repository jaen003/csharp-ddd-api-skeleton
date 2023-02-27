namespace Src.Core.Shared.Domain.Exceptions;

public class DomainException : Exception
{
    protected const int CRITICAL = 1;
    protected const int ERROR = 2;
    protected const int WARNING = 3;
    protected const int INFORMATION = 4;
    protected const int DEBUG = 5;

    public int Code { get; }
    private readonly int type;

    public DomainException(int code, int type, string message)
        : base(message)
    {
        Code = code;
        this.type = type;
    }

    public DomainException() { }

    public DomainException(string? message)
        : base(message) { }

    public DomainException(string? message, Exception? innerException)
        : base(message, innerException) { }

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
