using HR.Common.Utilities;

using System;
using System.Linq;
using System.Linq.Expressions;

namespace HR.Common.Persistence.Pagination
{
    public class SortProperty<T, TProperty> : SortProperty<T>
    {
        public SortProperty(Expression<Func<T, TProperty>> property, SortDirection direction)
            : base(direction)
        {
            Property = Ensure.Argument.NotNull(() => property);
        }

        public new Expression<Func<T, TProperty>> Property { get; }

        public override IOrderedQueryable<T> Sort(IQueryable<T> sequence)
        {
            return sequence.IsOrdered()
                ? Direction == SortDirection.Ascending
                    ? ((IOrderedQueryable<T>)sequence).ThenBy(Property)
                    : ((IOrderedQueryable<T>)sequence).ThenByDescending(Property)
                : Direction == SortDirection.Ascending
                    ? sequence.OrderBy(Property)
                    : sequence.OrderByDescending(Property);
        }
    }
}
