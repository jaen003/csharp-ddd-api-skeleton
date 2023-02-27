namespace Src.Core.Shared.Domain.Geneators;

public static class UuidGenerator
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
