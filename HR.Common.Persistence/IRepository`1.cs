using HR.Common.Persistence.Entities;

namespace HR.Common.Persistence
{
    /// <summary>
    /// Defines the interface for a class to implement in order to realize the Repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that is persisted through this repository.</typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
