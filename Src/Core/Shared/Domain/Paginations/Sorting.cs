using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations;

internal class Sorting
{
    private readonly SortingField @field;
    private readonly SortingType type;

    public string Field => @field.Value;

    private Sorting(SortingField field, SortingType type)
    {
        this.@field = field;
        this.type = type;
    }

    public bool IsDescending()
    {
        return type.IsDescending();
    }

    public static Sorting? Create(string? field, string? type)
    {
        if (field == null && type == null)
        {
            return null;
        }
        if (field == null)
        {
            throw new UnexpectedNullSortingField();
        }
        return new Sorting(new SortingField(field), new SortingType(type));
    }
}
