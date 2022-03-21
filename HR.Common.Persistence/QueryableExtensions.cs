using HR.Common.Utilities;

using System.Linq;
using System.Linq.Expressions;

namespace HR.Common.Persistence
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> sequence, IQueryableFilter<T> filter)
        {
            Ensure.Argument.NotNull(() => filter);

            return filter.Filter(sequence);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> sequence, IQueryablePaginator<T> paginator)
        {
            Ensure.Argument.NotNull(() => paginator);

            return paginator.Paginate(sequence);
        }

        public static bool IsOrdered<T>(this IQueryable<T> sequence) => OrderMethodFinder.Find(sequence.Expression);

        private class OrderMethodFinder : ExpressionVisitor
        {
            public bool IsFound { get; private set; }

            public static bool Find(Expression expression)
            {
                var finder = new OrderMethodFinder();
                finder.Visit(expression);
                return finder.IsFound;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (new[] { "OrderBy", "OrderByDescending", "ThenBy", "ThenByDescending" }.Contains(node.Method.Name))
                {
                    IsFound = true;
                }

                return base.VisitMethodCall(node);
            }
        }
    }
}
