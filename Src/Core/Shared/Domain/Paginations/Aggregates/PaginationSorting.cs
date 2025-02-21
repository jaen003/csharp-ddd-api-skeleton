using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.Paginations.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.Aggregates;

public class PaginationSorting
{
    private readonly PaginationSortingField @field;
    private readonly PaginationSortingType type;

    public string Field => @field.Value;

    private PaginationSorting(PaginationSortingField field, PaginationSortingType type)
    {
        this.@field = field;
        this.type = type;
    }

    public bool IsDescending()
    {
        return type.IsDescending();
    }

    public static PaginationSorting? Create(string? field, string? type)
    {
        if (field == null && type == null)
        {
            return null;
        }
        if (field == null)
        {
            throw new NullPaginationSortingFieldNotAllowedException();
        }
        return new PaginationSorting(
            new PaginationSortingField(field),
            new PaginationSortingType(type)
        );
    }
}
