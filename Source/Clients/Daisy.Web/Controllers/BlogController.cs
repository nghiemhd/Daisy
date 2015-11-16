using AutoMapper;
using Daisy.Common;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Web.Models;

namespace Daisy.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUrlRecordService urlRecordService;

        public BlogController(IBlogService blogService, IUrlRecordService urlRecordService)
        {
            this.blogService = blogService;
            this.urlRecordService = urlRecordService;
        }

        public ActionResult Index()
        {
            var searchOptions = new SearchBlogOptions
            {
                LanguageId = 1,
                IsPublished = true,
                PageSize = 10
            };
            var blogs = blogService.SearchBlogs(searchOptions);

            var blogsModel = Mapper.Map<List<DaisyModels.Blog>>(blogs.Items);

            foreach (var blog in blogsModel)
            {
                blog.Slug = urlRecordService.GetActiveSlug(blog.Id, typeof(DaisyEntities.BlogPost).Name, searchOptions.LanguageId.Value);
            }

            var pagedListBlogs = new PagedList<DaisyModels.Blog>(blogsModel, blogs.PageIndex, blogs.PageSize, blogs.TotalCount);
            var result = new DaisyModels.PagedListBlogViewModel
            {
                Blogs = pagedListBlogs
            };

            return View(result);
        }

        public ActionResult Detail(string slug)
        {
            var urlRecord = urlRecordService.GetUrlRecordBy(typeof(DaisyEntities.BlogPost).Name, slug, 1);
            if (urlRecord == null || !urlRecord.IsActive)
            {
                return PartialView("PageNotFound");
            }
            var blog = blogService.GetBlogBy(urlRecord.EntityId);
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