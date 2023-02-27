using Src.Core.Shared.Domain.Generators;
using IdGen;

namespace Src.Core.Shared.Infrastructure.Generators;

public class SnowflakeIdentifierGenerator : IIdentifierGenerator
{
    private readonly IdGenerator generator;

    public SnowflakeIdentifierGenerator(IdGenerator generator)
    {
        this.generator = generator;
    }

    public long Generate()
    {
        return generator.CreateId();
    }
}
