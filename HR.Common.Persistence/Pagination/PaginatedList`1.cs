using HR.Common.Utilities;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HR.Common.Persistence.Pagination
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public PaginatedList(IEnumerable<T> currentPage, SortingPaginator<T> paginator)
        {
            if (!(currentPage is IList<T> list))
            {
                list = Ensure.Argument.NotNull(() => currentPage).ToList();
            }
            InnerList = list;
            Paginator = Ensure.Argument.NotNull(() => paginator);
        }

        protected IList<T> InnerList { get; }
        protected SortingPaginator<T> Paginator { get; }

        public int PageNumber => Paginator.PageNumber;
        public int PageSize => Paginator.PageSize;
        public int PageCount => Paginator.PageCount;
        public int ItemCount => Paginator.ItemCount;
        public T this[int index] => InnerList[index];
        public int Count => InnerList.Count;
        public IEnumerator<T> GetEnumerator() => InnerList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
