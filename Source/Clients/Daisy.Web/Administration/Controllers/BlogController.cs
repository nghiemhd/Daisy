using DaisyModels = Daisy.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.ServiceContracts;
using Daisy.Service.DataContracts;
using Daisy.Common;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IContentService contentService;

        public BlogController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DaisyModels.Blog model)
        {
            var entity = Mapper.Map<DaisyEntities.Blog>(model);
            contentService.UpdateBlog(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(DaisyModels.Blog model)
        {
            var entity = Mapper.Map<DaisyEntities.Blog>(model);
            contentService.UpdateBlog(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(DaisyModels.SearchBlogModel options)
        {
            try
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }
                var searchOptions = Mapper.Map<SearchBlogOptions>(options);
                var blogs = contentService.SearchBlogs(searchOptions);

                var blogsModel = Mapper.Map<List<DaisyModels.Blog>>(blogs.Items);
                var pagedListBlogs = new PagedList<DaisyModels.Blog>(blogsModel, blogs.PageIndex, blogs.PageSize, blogs.TotalCount);
                var result = new DaisyModels.PagedListBlogViewModel
                {
                    Blogs = pagedListBlogs,
                    SearchOptions = options
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}