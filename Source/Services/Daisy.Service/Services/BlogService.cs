using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Core.Caching;
using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service
{
    public class BlogService : HandleErrorService, IBlogService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.BlogPost> blogRepository;
        private ILogger logger;
        private ICacheManager cacheManager;

        public BlogService(IUnitOfWork unitOfWork, ILogger logger, ICacheManager cacheManager)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.cacheManager = cacheManager;
            this.blogRepository = this.unitOfWork.GetRepository<BlogPost>();
        }

        public void UpdateBlog(DaisyEntities.BlogPost entity)
        {
            Process(() =>
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (entity.Id <= 0)
                {
                    var blog = blogRepository.Insert(entity);
                }
                else
                {
                    blogRepository.Update(entity);
                }

                this.unitOfWork.Commit();
            });
        }

        public Daisy.Common.PagedList<DaisyEntities.BlogPost> SearchBlogs(SearchBlogOptions options)
        {
            var blogs = Process(() =>
            {
                var query = blogRepository.Query();

                if (!options.Title.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Title.Contains(options.Title));
                }

                if (options.IsPublished != null)
                {
                    query = query.Where(x => x.IsPublished == options.IsPublished);
                }

                if (options.FromCreatedDate != null)
                {
                    query = query.Where(x => DbFunctions.TruncateTime(x.CreatedDate) >= options.FromCreatedDate);
                }

                if (options.ToCreatedDate != null)
                {
                    query = query.Where(x => DbFunctions.TruncateTime(x.CreatedDate) <= options.ToCreatedDate);
                }

                if (options.LanguageId != null)
                {
                    query = query.Where(x => x.LanguageId == options.LanguageId.Value);
                }

                if (options.PageSize <= 0 || options.PageSize > Constants.MaxPageSize)
                {
                    options.PageSize = Constants.DefaultPageSize;
                }

                if (options.PageIndex < 0)
                {
                    options.PageIndex = 0;
                }

                int totalCount = query.Count();
                query = query
                        .OrderByDescending(x => x.Id)
                        .Skip(options.PageSize * options.PageIndex)
                        .Take(options.PageSize);

                var result = new PagedList<DaisyEntities.BlogPost>(
                    query.ToList(),
                    options.PageIndex,
                    options.PageSize,
                    totalCount
                );

                return result;
            });

            return blogs as PagedList<DaisyEntities.BlogPost>;
        }

        public void PublishBlogs(IList<int> blogIds, bool isPublished)
        {
            Process(() =>
            {
                var updatedBlogs = blogRepository.Query()
                    .Where(x => blogIds.Contains(x.Id) && x.IsPublished != isPublished).ToList();
                updatedBlogs.ForEach(blog =>
                {
                    blog.IsPublished = isPublished;
                    blog.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                    blog.UpdatedDate = DateTime.Now;
                });

                this.unitOfWork.Commit();
            });
        }

        public DaisyEntities.BlogPost GetBlogBy(int id)
        {
            var blog = Process(() =>
            {
                return blogRepository.Query().Where(x => x.Id == id).FirstOrDefault();
            });
            return blog as DaisyEntities.BlogPost;
        }
    }
}
