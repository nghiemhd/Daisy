using AutoMapper;
using Daisy.Common;
using Daisy.Logging.Extensions;
using Daisy.Service.Common;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Admin.Models;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IContentService contentService;
        private readonly ILocalizationService localizationService;

        public BlogController(IContentService contentService, ILocalizationService localizationService)
        {
            this.contentService = contentService;
            this.localizationService = localizationService;
        }

        public ActionResult Index()
        {
            ViewData["languages"] = this.localizationService.GetLanguages().ToList();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var blog = contentService.GetBlogBy(id);
            var model = Mapper.Map<DaisyModels.Blog>(blog);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] DaisyModels.Blog model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<DaisyEntities.BlogPost>(model);
                contentService.UpdateBlog(entity);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DaisyModels.Blog model)
        {
            if (ModelState.IsValid)
            {
                var entity = contentService.GetBlogBy(model.Id);
                entity.Title = model.Title;
                entity.Highlight = model.Highlight;
                entity.ImageUrl = model.ImageUrl;
                entity.IsPublished = model.IsPublished;
                entity.Content = model.Content;
                contentService.UpdateBlog(entity);
                TempData["message"] = "Update successfully";
            }
            return View(model);
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

        [HttpPost]
        public JsonResult Publish(int[] blogIds, bool isPublished)
        {
            try
            {
                contentService.PublishBlogs(blogIds, isPublished);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }
    }
}