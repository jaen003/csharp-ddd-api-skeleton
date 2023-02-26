namespace Src.Core.Shared.Domain.Geneators;

public static class TimestampGenerator
{
    public static int Generate()
    {
        DateTime dateNow = DateTime.UtcNow;
        return (int)dateNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}
