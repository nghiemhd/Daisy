using Daisy.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Common
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
            {
                TotalPages++;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public IEnumerable<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get
            {
                return this.PageIndex > 0;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return this.PageIndex + 1 < this.TotalPages;
            }
        }
    }
}
