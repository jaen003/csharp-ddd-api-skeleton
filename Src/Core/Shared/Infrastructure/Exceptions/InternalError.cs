using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public abstract class InternalError : CustomException
{
    private const int CODE = 2;

    protected InternalError(string message)
        : base(CODE, CRITICAL, message) { }
}
