using HR.Common.Persistence.Entities;
using HR.Common.Utilities;

using Microsoft.EntityFrameworkCore;

using System;

namespace HR.Common.Persistence.EntityFrameworkCore
{
    public class RepositoryFactory<TDbContext> : IRepositoryFactory<TDbContext>
        where TDbContext : DbContext
    {
        IRepository<TEntity> IRepositoryFactory.Create<TEntity>(IUnitOfWork uow)
            => Create<TEntity>((IUnitOfWork<TDbContext>)uow);

        public virtual IRepository<TEntity> Create<TEntity>(IUnitOfWork<TDbContext> uow)
            where TEntity : class, IEntity
        {
            Ensure.Argument.NotNull(() => uow);

            var repositoryType = GetRepositoryImplementationType<TEntity>();
            var repository = Activator.CreateInstance(repositoryType, new[] { uow }) as IRepository<TEntity>;

            return repository;
        }

        protected virtual Type GetRepositoryImplementationType<TEntity>()
            where TEntity : class, IEntity => typeof(Repository<TEntity, TDbContext>);
    }
}
