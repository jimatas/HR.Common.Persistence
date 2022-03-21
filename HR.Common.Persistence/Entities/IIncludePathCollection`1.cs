using System.Collections.Generic;

namespace HR.Common.Persistence.Entities
{
    /// <summary>
    /// Collection of strings that specify the navigation properties of an entity to eager load.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity whose properties to eager load.</typeparam>
    public interface IIncludePathCollection<TEntity> : IEnumerable<string>
        where TEntity : IEntity
    {
        void Add(string path);
        bool Remove(string path);
    }
}
