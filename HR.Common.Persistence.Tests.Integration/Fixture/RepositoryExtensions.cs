using HR.Common.Persistence.Entities;
using HR.Common.Utilities;

using System.Collections.Generic;

namespace HR.Common.Persistence.Tests.Integration.Fixture
{
    public static class RepositoryExtensions
    {
        public static void AddRange<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities)
            where TEntity : class, IEntity
        {
            Ensure.Argument.NotNull(() => entities);

            foreach (TEntity entity in entities)
            {
                repository.Add(entity);
            }
        }
    }
}
