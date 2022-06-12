﻿using HR.Common.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.Common.Persistence.Pagination
{
    public class SortingPaginator<T> : IQueryablePaginator<T>
    {
        public const int DefaultPageSize = 20;

        private int pageNumber;
        private int pageSize;
        private int itemCount;

        public SortingPaginator() : this(pageNumber: 1) { }
        public SortingPaginator(int pageNumber) : this(pageNumber, DefaultPageSize) { }
        public SortingPaginator(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = Ensure.Argument.NotOutOfRange(value, nameof(PageNumber), message: "Value cannot be negative or zero.", 1, null);
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = Ensure.Argument.NotOutOfRange(value, nameof(PageSize), message: "Value cannot be negative or zero.", 1, null);
        }

        public int PageCount { get; private set; }

        public int ItemCount
        {
            get => itemCount;
            private set
            {
                itemCount = value;
                PageCount = (int)Math.Ceiling((double)itemCount / PageSize);
            }
        }

        public ICollection<ISortDirective<T>> SortDirectives { get; } = new List<ISortDirective<T>>();

        public IQueryable<T> Paginate(IQueryable<T> sequence)
        {
            ItemCount = sequence.Count();

            IOrderedQueryable<T> orderedSequence = null;
            foreach (var directive in SortDirectives)
            {
                orderedSequence = directive.Sort(orderedSequence ?? sequence);
            }

            if (orderedSequence != null)
            {
                sequence = orderedSequence.Skip((PageNumber - 1) * PageSize);
            }
            sequence = sequence.Take(PageSize);

            return sequence;
        }

        public static SortingPaginator<T> operator ++(SortingPaginator<T> paginator)
        {
            paginator.PageNumber++;
            return paginator;
        }

        public static SortingPaginator<T> operator --(SortingPaginator<T> paginator)
        {
            paginator.PageNumber--;
            return paginator;
        }
    }
}
