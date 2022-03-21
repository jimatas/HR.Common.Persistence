namespace HR.Common.Persistence.Entities
{
    /// <summary>
    /// Represents an entity that is retrieved and persisted through a repository.
    /// </summary>
    public interface IEntity
    {
        bool IsTransient { get; }
    }
}
