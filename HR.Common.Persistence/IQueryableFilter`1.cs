using System.Linq;

namespace HR.Common.Persistence
{
    public interface IQueryableFilter<T>
    {
        IQueryable<T> Filter(IQueryable<T> sequence);
    }
}
