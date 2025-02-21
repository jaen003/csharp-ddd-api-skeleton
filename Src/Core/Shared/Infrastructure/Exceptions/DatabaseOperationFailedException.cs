using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class DatabaseOperationFailedException : InfrastructureException
{
    public DatabaseOperationFailedException(string messageDetails)
        : base(
            CustomExceptionCode.DatabaseOperationFailed,
            $"The database operation failed: {messageDetails}."
        ) { }
}
