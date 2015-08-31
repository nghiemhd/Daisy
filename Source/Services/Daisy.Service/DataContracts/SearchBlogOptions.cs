using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class SearchBlogOptions
    {
        public string Title { get; set; }

        public bool? IsPublished { get; set; }

        public DateTime? FromCreatedDate { get; set; }

        public DateTime? ToCreatedDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
