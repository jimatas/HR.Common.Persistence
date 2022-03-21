using HR.Common.Persistence.Entities;
using HR.Common.Utilities;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Common.Persistence
{
    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        public event EventHandler<UnitOfWorkCompletedEventArgs> Completed;

        private readonly IDictionary<Type, RepositoryWrapper> repositories = new Dictionary<Type, RepositoryWrapper>();
        private readonly IRepositoryFactory repositoryFactory;

        protected UnitOfWorkBase(IRepositoryFactory repositoryFactory)
            => this.repositoryFactory = Ensure.Argument.NotNull(() => repositoryFactory);

        public abstract bool IsTransactional { get; }

        public abstract void BeginTransaction();
        public virtual Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            BeginTransaction();
            return Task.CompletedTask;
        }

        public abstract void Complete();
        public abstract Task CompleteAsync(CancellationToken cancellationToken = default);

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntity
        {
            if (!repositories.TryGetValue(typeof(TEntity), out RepositoryWrapper wrapper))
            {
                wrapper = new RepositoryWrapper(repositoryFactory.Create<TEntity>(this));
                repositories.Add(typeof(TEntity), wrapper);
            }
            return wrapper.Repository<TEntity>();
        }

        protected void OnCompleted(UnitOfWorkCompletedEventArgs e) => Completed?.Invoke(this, e);

        #region IDisposable support
        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    repositories.Clear();
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private readonly struct RepositoryWrapper
        {
            private readonly object repository;
            public RepositoryWrapper(object repository) => this.repository = Ensure.Argument.NotNull(() => repository);
            public IRepository<TEntity> Repository<TEntity>()
                where TEntity : class, IEntity => (IRepository<TEntity>)repository;
        }
    }
}
