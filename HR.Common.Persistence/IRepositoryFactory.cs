using HR.Common.Persistence.Entities;

namespace HR.Common.Persistence
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>(IUnitOfWork uow)
            where TEntity : class, IEntity;
    }
}
