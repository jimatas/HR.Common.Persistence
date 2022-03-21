using System.Linq;

namespace HR.Common.Persistence.Pagination
{
    public interface ISortDirective<T>
    {
        IOrderedQueryable<T> Sort(IQueryable<T> sequence);
    }
}
