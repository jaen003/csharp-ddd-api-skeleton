using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Application.Exceptions;

public abstract class CustomApplicationException : CustomException
{
    protected CustomApplicationException(CustomExceptionCode code, string message)
        : base(code, CustomExceptionSeverityLevel.Warning, message) { }
}
