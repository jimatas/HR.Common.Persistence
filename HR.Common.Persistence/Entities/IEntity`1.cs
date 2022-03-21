using System;

namespace HR.Common.Persistence.Entities
{
    /// <summary>
    /// Extends the <see cref="IEntity"/> interface adding a unique Id property.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the Id property.</typeparam>
    public interface IEntity<TIdentifier> : IEntity
        where TIdentifier : IEquatable<TIdentifier>
    {
        TIdentifier Id { get; }
    }
}
