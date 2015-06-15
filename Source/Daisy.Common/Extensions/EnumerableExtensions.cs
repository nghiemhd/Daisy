using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            if (pageSize <= 0)
            {
                pageSize = Constants.DefaultPageSize;
            }

            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
