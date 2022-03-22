using System.Linq;

namespace HR.Common.Persistence
{
    /// <summary>
    /// Defines the interface for a class that paginates, after optionally sorting the elements of a sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryablePaginator<T>
    {
        IQueryable<T> Paginate(IQueryable<T> sequence);
    }
}
