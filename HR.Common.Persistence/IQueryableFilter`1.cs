using System.Linq;

namespace HR.Common.Persistence
{
    /// <summary>
    /// Defines the interface for a class that filters the elements of a sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryableFilter<T>
    {
        IQueryable<T> Filter(IQueryable<T> sequence);
    }
}
