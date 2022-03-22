using HR.Common.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Common.Persistence.EntityFrameworkCore
{
    public class UnitOfWork<TDbContext> : UnitOfWorkBase, IUnitOfWork<TDbContext>
        where TDbContext : DbContext, new()
    {
        private IDbContextTransaction dbContextTransaction;
        private readonly bool disposeOfDbContext;

        public UnitOfWork() : this(new RepositoryFactory<TDbContext>()) { }
        public UnitOfWork(IRepositoryFactory<TDbContext> repositoryFactory)
            : this(new TDbContext(), repositoryFactory) => disposeOfDbContext = true;

        public UnitOfWork(TDbContext dbContext) : this(dbContext, new RepositoryFactory<TDbContext>()) { }
        public UnitOfWork(TDbContext dbContext, IRepositoryFactory<TDbContext> repositoryFactory) : base(repositoryFactory)
            => DbContext = Ensure.Argument.NotNull(() => dbContext);

        public TDbContext DbContext { get; }
        public override bool IsTransactional => dbContextTransaction is not null;

        public override void Complete()
        {
            try
            {
                DbContext.ValidateChangedEntities();
                DbContext.SaveChanges();
                CommitTransaction();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            OnCompleted(new UnitOfWorkCompletedEventArgs(this));
        }

        public override async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                DbContext.ValidateChangedEntities();
                await DbContext.SaveChangesAsync(cancellationToken).WithoutCapturingContext();
                await CommitTransactionAsync(cancellationToken).WithoutCapturingContext();
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken).WithoutCapturingContext();
                throw;
            }
            OnCompleted(new UnitOfWorkCompletedEventArgs(this));
        }

        public override void BeginTransaction()
        {
            if (IsTransactional)
            {
                throw new InvalidOperationException("An active transaction is already in progress. Nested transactions are not supported.");
            }
            dbContextTransaction = DbContext.Database.BeginTransaction();
        }

        public override async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (IsTransactional)
            {
                throw new InvalidOperationException("An active transaction is already in progress. Nested transactions are not supported.");
            }
            dbContextTransaction = await DbContext.Database.BeginTransactionAsync(cancellationToken).WithoutCapturingContext();
        }

        protected void CommitTransaction()
        {
            if (IsTransactional)
            {
                dbContextTransaction.Commit();
                dbContextTransaction.Dispose();
                dbContextTransaction = null;
            }
        }

        protected void RollbackTransaction()
        {
            if (IsTransactional)
            {
                dbContextTransaction.Rollback();
                dbContextTransaction.Dispose();
                dbContextTransaction = null;
            }
        }

        protected async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (IsTransactional)
            {
                await dbContextTransaction.CommitAsync(cancellationToken).WithoutCapturingContext();
                await dbContextTransaction.DisposeAsync().WithoutCapturingContext();
                dbContextTransaction = null;
            }
        }

        protected async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (IsTransactional)
            {
                await dbContextTransaction.RollbackAsync(cancellationToken).WithoutCapturingContext();
                await dbContextTransaction.DisposeAsync().WithoutCapturingContext();
                dbContextTransaction = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    RollbackTransaction();
                    if (disposeOfDbContext)
                    {
                        DbContext.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}
