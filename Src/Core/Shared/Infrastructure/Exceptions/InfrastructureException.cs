using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public abstract class InfrastructureException : CustomException
{
    protected InfrastructureException(CustomExceptionCode code, string message)
        : base(code, SeverityLevel.Critical, message) { }
}
