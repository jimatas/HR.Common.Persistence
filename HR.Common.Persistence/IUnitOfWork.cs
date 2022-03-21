using HR.Common.Persistence.Entities;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Common.Persistence
{
    /// <summary>
    /// Defines the interface for a class to implement in order to realize the Unit of Work pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        event EventHandler<UnitOfWorkCompletedEventArgs> Completed;

        /// <summary>
        /// Indicates whether an explicit database transaction has been started on this unit of work.
        /// </summary>
        bool IsTransactional { get; }

        /// <summary>
        /// Starts an explicit database transaction on this unit of work, if supported by the persistence store.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Starts an explicit database transaction on this unit of work, if supported by the persistence store.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        void Complete();
        Task CompleteAsync(CancellationToken cancellationToken = default);

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntity;
    }
}
