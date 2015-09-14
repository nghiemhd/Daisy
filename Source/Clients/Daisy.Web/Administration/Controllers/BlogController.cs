using AutoMapper;
using Daisy.Common;
using Daisy.Common.Extensions;
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
        private readonly IBlogService blogService;
        private readonly ILocalizationService localizationService;
        private readonly IUrlRecordService urlRecordService;

        public BlogController(IBlogService blogService, ILocalizationService localizationService, IUrlRecordService urlRecordService)
        {
            this.blogService = blogService;
            this.localizationService = localizationService;
            this.urlRecordService = urlRecordService;
        }

        public ActionResult Index()
        {
            ViewData["languages"] = this.localizationService.GetLanguages().ToList();
            return View();
        }

        public ActionResult Create()
        {
            var languages = GetLanguages();
            var model = new DaisyModels.Blog 
            { 
                Languages = languages
            };
            return View(model);
        }

        private List<DaisyModels.Language> GetLanguages()
        {
            var languages = Mapper.Map<List<DaisyModels.Language>>(this.localizationService.GetLanguages().ToList());
            return languages;
        }

        public ActionResult Edit(int id)
        {            
            var languages = GetLanguages();
            var blog = blogService.GetBlogBy(id);
            var slug = urlRecordService.GetActiveSlug(blog.Id, typeof(DaisyEntities.BlogPost).Name, blog.LanguageId);
            var model = Mapper.Map<DaisyModels.Blog>(blog);
            model.Languages = languages;
            model.Slug = slug;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] DaisyModels.Blog model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<DaisyEntities.BlogPost>(model);
                blogService.UpdateBlog(entity);

                if (model.Slug.IsNullOrEmpty())
                {
                    model.Slug = model.Title;
                }
                urlRecordService.SaveSlug<DaisyEntities.BlogPost>(entity, model.Slug, model.LanguageId);

                return RedirectToAction("Index");
            }

            var languages = GetLanguages();
            model.Languages = languages;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DaisyModels.Blog model)
        {
            if (ModelState.IsValid)
            {
                var entity = blogService.GetBlogBy(model.Id);
                entity.LanguageId = model.LanguageId;
                entity.Title = model.Title;
                entity.Highlight = model.Highlight;
                entity.ImageUrl = model.ImageUrl;
                entity.IsPublished = model.IsPublished;
                entity.Content = model.Content;

                entity.MetaDescription = model.MetaDescription;
                entity.MetaKeywords = model.MetaKeywords;
                entity.MetaTitle = model.MetaTitle;
                entity.Tags = model.Tags;

                blogService.UpdateBlog(entity);
                if (model.Slug.IsNullOrEmpty())
                {
                    model.Slug = model.Title;
                }
                model.Slug = urlRecordService.SaveSlug<DaisyEntities.BlogPost>(entity, model.Slug, model.LanguageId);
                TempData["message"] = "Update successfully";
                ModelState.Clear();
            }
            var languages = GetLanguages();
            model.Languages = languages;
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
                var blogs = blogService.SearchBlogs(searchOptions);
                var languages = localizationService.GetLanguages().ToList();
                var blogsModel = Mapper.Map<List<DaisyModels.Blog>>(blogs.Items);
                foreach (var blog in blogsModel)
                {
                    blog.Language = languages.Where(x => x.Id == blog.LanguageId).Select(x => x.Name).FirstOrDefault();
                }
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
                blogService.PublishBlogs(blogIds, isPublished);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }
    }
}