namespace HR.Common.Persistence.Entities
{
    /// <summary>
    /// Collection of strings that specify the navigation properties of an entity to eager load.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity whose properties to eager load.</typeparam>
    /// <typeparam name="TProperty">The type of the navigation property.</typeparam>
    public interface IIncludePathCollection<TEntity, out TProperty> : IIncludePathCollection<TEntity>
        where TEntity : IEntity
    {
        new void Add(string path);
    }
}
