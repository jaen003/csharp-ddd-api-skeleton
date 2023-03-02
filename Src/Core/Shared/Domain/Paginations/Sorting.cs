using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations;

public class Sorting
{
    public SortingField Field { get; }
    public SortingType Type { get; }

    public Sorting(SortingField field, SortingType type)
    {
        Field = field;
        Type = type;
    }

    public static Sorting? Create(string? field, string? type)
    {
        if (field == null && type == null)
        {
            return null;
        }
        if (field == null)
        {
            throw new NullSortingFieldException();
        }
        return new Sorting(new SortingField(field), new SortingType(type));
    }
}
