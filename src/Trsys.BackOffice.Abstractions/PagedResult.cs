using System.Collections.Generic;

namespace Trsys.BackOffice.Abstractions
{
    public class PagedResult<T>
    {
        public PagedResult(int page, int perPage, int totalCount, List<T> items)
        {
            Page = page;
            PerPage = perPage;
            TotalCount = totalCount;
            Items = items;
        }

        public int Page { get; }
        public int PerPage { get; }
        public int TotalCount { get; }
        public List<T> Items { get; }
    }
}