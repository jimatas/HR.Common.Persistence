using HR.Common.Utilities;

using System.Collections;
using System.Collections.Generic;

namespace HR.Common.Persistence.Entities
{
    public class IncludePathCollection<TEntity> : IIncludePathCollection<TEntity>
        where TEntity : IEntity
    {
        private readonly List<string> paths;

        public IncludePathCollection() : this(new List<string>()) { }
        internal IncludePathCollection(List<string> paths) => this.paths = paths;

        public IEnumerator<string> GetEnumerator() => paths.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(string path)
        {
            Ensure.Argument.NotNullOrWhiteSpace(() => path);

            paths.Add(path);
        }

        public bool Remove(string path)
        {
            Ensure.Argument.NotNullOrWhiteSpace(() => path);

            var i = paths.LastIndexOf(path);
            if (i >= 0)
            {
                paths.RemoveAt(i);
                return true;
            }
            return false;
        }
    }
}
