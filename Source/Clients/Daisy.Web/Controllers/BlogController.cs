using AutoMapper;
using Daisy.Common;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaisyModels = Daisy.Web.Models;

namespace Daisy.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IContentService contentService;

        public BlogController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public ActionResult Index()
        {
            var searchOptions = new SearchBlogOptions
            {
                IsPublished = true,
                PageSize = 10
            };
            var blogs = contentService.SearchBlogs(searchOptions);

            var blogsModel = Mapper.Map<List<DaisyModels.Blog>>(blogs.Items);
            var pagedListBlogs = new PagedList<DaisyModels.Blog>(blogsModel, blogs.PageIndex, blogs.PageSize, blogs.TotalCount);
            var result = new DaisyModels.PagedListBlogViewModel
            {
                Blogs = pagedListBlogs
            };

            return View(result);
        }

        [Route("blog/{blogId:int}")]
        public ActionResult Detail(int blogId)
        {
            var blog = contentService.GetBlogBy(blogId);
            if (blog == null || !blog.IsPublished)
            {
                return PartialView("PageNotFound");
            }

            var blogModel = Mapper.Map<DaisyModels.Blog>(blog);

            var model = new DaisyModels.BlogDetailViewModel
            {
                Blog = blogModel
            };

            return View(model);
        }
    }
}