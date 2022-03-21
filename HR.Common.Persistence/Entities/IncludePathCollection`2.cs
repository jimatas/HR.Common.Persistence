using HR.Common.Utilities;

using System.Collections.Generic;
using System.Linq;

namespace HR.Common.Persistence.Entities
{
    internal class IncludePathCollection<TEntity, TProperty> : IncludePathCollection<TEntity>, IIncludePathCollection<TEntity, TProperty>
        where TEntity : IEntity
    {
        public IncludePathCollection() : base() { }
        public IncludePathCollection(List<string> paths) : base(paths) { }

        void IIncludePathCollection<TEntity, TProperty>.Add(string path)
        {
            Ensure.Argument.NotNullOrWhiteSpace(() => path);

            var previousPath = this.LastOrDefault();
            if (previousPath != null && Remove(previousPath))
            {
                path = $"{previousPath}.{path}";
            }
            Add(path);
        }
    }
}
