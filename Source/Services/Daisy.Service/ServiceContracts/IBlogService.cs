using Daisy.Common;
using Daisy.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service.ServiceContracts
{
    public interface IBlogService
    {
        void UpdateBlog(DaisyEntities.BlogPost entity);

        PagedList<DaisyEntities.BlogPost> SearchBlogs(SearchBlogOptions options);

        void PublishBlogs(IList<int> blogIds, bool isPublished);

        DaisyEntities.BlogPost GetBlogBy(int id);
    }
}
