using HR.Common.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.Common.Persistence.Pagination
{
    internal static class SortPropertyHelper<T>
    {
        public static IEnumerable<SortProperty<T>> ParseFromString(string value)
        {
            Ensure.Argument.NotNullOrWhiteSpace(() => value);

            foreach (var directive in value.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()))
            {
                var propertyName = directive;

                bool descending;
                if ((descending = directive.StartsWith("-")) || directive.StartsWith("+"))
                {
                    propertyName = directive.Substring(1).TrimStart('(').TrimEnd(')');
                }

                var direction = descending ? SortDirection.Descending : SortDirection.Ascending;

                SortProperty<T> property;
                try
                {
                    property = new SortProperty<T>(propertyName, direction);
                }
                catch (ArgumentException exception)
                {
                    throw new FormatException("At least one of the sorting directives in the input string could not be parsed successfully.", exception);
                }

                yield return property;
            }
        }
    }
}
