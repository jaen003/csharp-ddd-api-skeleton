namespace Src.Core.Shared.Domain.Generators;

public static class UuidGenerator
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
