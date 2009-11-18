using System;
using System.Collections.Generic;

namespace BalloonShop.Infrastructure
{
    public class PagedList<T> : List<T>
    {
        public PagedList(int page, int pageSize, int pageCount, IEnumerable<T> list)
        {
            Page = page;
            PageSize = pageSize;
            NumberOfPages = pageCount;
            AddRange(list);
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPages { get; set; }

        public bool HasPreviousPage
        {
            get { return Page > 1; }
        }
        public bool HasNextPage
        {
            get { return Page < NumberOfPages; }
        }
    }
}
