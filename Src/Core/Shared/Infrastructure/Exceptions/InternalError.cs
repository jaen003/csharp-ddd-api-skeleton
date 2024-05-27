using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public abstract class InternalError : ApplicationException
{
    private const int CODE = 2;

    protected InternalError(string message)
        : base(CODE, CRITICAL, message) { }
}
