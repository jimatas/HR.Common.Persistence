using System.Collections.Generic;

namespace HR.Common.Persistence.Pagination
{
    public interface IPaginatedList<T> : IReadOnlyList<T>
    {
        int PageNumber { get; }
        int PageSize { get; }
        int PageCount { get; }
        int ItemCount { get; }
    }
}
