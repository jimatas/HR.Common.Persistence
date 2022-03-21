using HR.Common.Persistence.Entities;

using Microsoft.EntityFrameworkCore;

namespace HR.Common.Persistence.EntityFrameworkCore
{
    public interface IRepositoryFactory<TDbContext> : IRepositoryFactory
        where TDbContext : DbContext
    {
        IRepository<TEntity> Create<TEntity>(IUnitOfWork<TDbContext> uow)
            where TEntity : class, IEntity;
    }
}
