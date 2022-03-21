using HR.Common.Persistence.Entities;
using HR.Common.Utilities;

using System;

namespace HR.Common.Persistence.InMemory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public virtual IRepository<TEntity> Create<TEntity>(IUnitOfWork uow)
            where TEntity : class, IEntity
        {
            Ensure.Argument.NotNull(() => uow);

            var repositoryType = GetRepositoryImplementationType<TEntity>();
            var repository = Activator.CreateInstance(repositoryType, new[] { uow }) as IRepository<TEntity>;

            return repository;
        }

        protected virtual Type GetRepositoryImplementationType<TEntity>()
            where TEntity : class, IEntity => typeof(Repository<TEntity>);
    }
}
