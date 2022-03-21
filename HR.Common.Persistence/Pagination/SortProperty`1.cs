using HR.Common.Utilities;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HR.Common.Persistence.Pagination
{
    public class SortProperty<T> : ISortDirective<T>
    {
        public SortProperty(string propertyName, SortDirection direction)
            : this(direction)
        {
            Ensure.Argument.NotNullOrWhiteSpace(() => propertyName);

            Property = GetPropertySelector(propertyName);
        }

        protected SortProperty(SortDirection direction) => Direction = direction;

        public LambdaExpression Property { get; }
        public SortDirection Direction { get; }

        public virtual IOrderedQueryable<T> Sort(IQueryable<T> sequence)
        {
            var sortMethodName = sequence.IsOrdered()
                ? Direction == SortDirection.Ascending
                    ? "ThenBy"
                    : "ThenByDescending"
                : Direction == SortDirection.Ascending
                    ? "OrderBy"
                    : "OrderByDescending";

            var sortMethod = typeof(Queryable).GetMethods().Single(method => method.Name == sortMethodName && method.GetParameters().Length == 2);
            sortMethod = sortMethod.MakeGenericMethod(typeof(T), Property.ReturnType);

            return (IOrderedQueryable<T>)sortMethod.Invoke(null, new object[] { sequence, Property });
        }

        private static LambdaExpression GetPropertySelector(string propertyName)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");

            Expression expression = parameter;
            foreach (var nestedProperty in propertyName.Split('.'))
            {
                var property = type.GetProperty(nestedProperty, BindingFlags.Public | BindingFlags.Instance);
                if (property is null)
                {
                    throw new ArgumentException($"No property '{nestedProperty}' on type '{type.Name}'.", nameof(propertyName));
                }

                expression = Expression.Property(expression, property);
                type = property.PropertyType;
            }

            expression = Expression.Lambda(expression, parameter);
            return expression as LambdaExpression;
        }
    }
}
