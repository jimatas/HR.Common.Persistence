using HR.Common.Persistence.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Common.Persistence
{
    /// <summary>
    /// Defines the interface for a class to implement in order to realize the Repository pattern.
    /// This interface declares only the retrieval methods and is extended by the <see cref="IRepository{TEntity}"/> interface, which declares the storage methods.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that is persisted through this repository.</typeparam>
    public interface IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        #region Synchronous methods
        int Count();
        int Count(IQueryableFilter<TEntity> filter);
        IEnumerable<TEntity> Find(IQueryableFilter<TEntity> filter);
        IEnumerable<TEntity> Find(IQueryableFilter<TEntity> filter, IIncludePathCollection<TEntity> includePaths);
        IEnumerable<TEntity> Find(IQueryableFilter<TEntity> filter, IQueryablePaginator<TEntity> paginator);
        IEnumerable<TEntity> Find(IQueryableFilter<TEntity> filter, IQueryablePaginator<TEntity> paginator, IIncludePathCollection<TEntity> includePaths);
        #endregion

        #region Asynchronous methods
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(IQueryableFilter<TEntity> filter, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(IQueryableFilter<TEntity> filter, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(IQueryableFilter<TEntity> filter, IIncludePathCollection<TEntity> includePaths, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(IQueryableFilter<TEntity> filter, IQueryablePaginator<TEntity> paginator, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(IQueryableFilter<TEntity> filter, IQueryablePaginator<TEntity> paginator, IIncludePathCollection<TEntity> includePaths, CancellationToken cancellationToken = default);
        #endregion
    }
}
