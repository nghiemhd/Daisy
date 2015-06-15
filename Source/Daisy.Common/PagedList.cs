using Daisy.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Common
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize) :
            this(source.GetPage(pageIndex, pageSize), pageIndex, pageSize, source.Count())
        {
        }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            this.TotalCount = totalCount;
            this.TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
            {
                this.TotalPages++;
            }

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source.ToList());
        }

        public int PageIndex
        {
            get;
            private set;
        }
        public int PageSize
        {
            get;
            private set;
        }
        public int TotalCount
        {
            get;
            private set;
        }
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
