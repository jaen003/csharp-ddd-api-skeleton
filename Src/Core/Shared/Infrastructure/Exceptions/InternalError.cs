using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public abstract class InternalError : CustomException
{
    protected InternalError(string message)
        : base(CustomExceptionCode.InternalError, SeverityLevel.Critical, message) { }
}
