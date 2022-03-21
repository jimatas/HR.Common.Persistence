using System.Linq;

namespace HR.Common.Persistence
{
    public interface IQueryablePaginator<T>
    {
        IQueryable<T> Paginate(IQueryable<T> sequence);
    }
}
