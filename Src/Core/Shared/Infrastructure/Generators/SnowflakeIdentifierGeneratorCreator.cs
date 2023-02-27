using IdGen;

namespace Src.Core.Shared.Infrastructure.Generators;

public class SnowflakeIdentifierGeneratorCreator
{
    private const int GENERATOR_ID = 0;
    private const int TIMESTAMP_BITS = 45;
    private const int GENERATOR_ID_BITS = 2;
    private const int SEQUENCE_BITS = 16;

    private readonly IdGeneratorOptions generatorOptions;

    public SnowflakeIdentifierGeneratorCreator()
    {
        generatorOptions = CreateGeneratorOptions();
    }

    public SnowflakeIdentifierGenerator Create()
    {
        IdGenerator generator = new(GENERATOR_ID, generatorOptions);
        return new(generator);
    }

    private static IdGeneratorOptions CreateGeneratorOptions()
    {
        DefaultTimeSource timeSource = GenerateTimeSource();
        IdStructure structure = new(TIMESTAMP_BITS, GENERATOR_ID_BITS, SEQUENCE_BITS);
        return new(structure, timeSource);
    }

    private static DefaultTimeSource GenerateTimeSource()
    {
        string startDateString = Environment.GetEnvironmentVariable(
            "IDENTIFIER_GENERATOR_START_DATE"
        )!;
        DateTime startTime = DateTime.ParseExact(startDateString, "yyyy-MM-dd", null);
        startTime = DateTime.SpecifyKind(startTime, DateTimeKind.Utc);
        return new(startTime);
    }
}
