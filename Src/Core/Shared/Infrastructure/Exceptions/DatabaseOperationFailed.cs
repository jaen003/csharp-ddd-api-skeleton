using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class DatabaseOperationFailed : InfrastructureException
{
    public DatabaseOperationFailed(string messageDetails)
        : base(
            CustomExceptionCode.DatabaseOperationFailed,
            $"The database operation failed: {messageDetails}."
        ) { }
}
