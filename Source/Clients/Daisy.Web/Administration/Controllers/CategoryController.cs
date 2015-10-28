using AutoMapper;
using Daisy.Common.Extensions;
using Daisy.Logging.Extensions;
using Daisy.Service.Common;
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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ILocalizationService localizationService;
        private readonly IUrlRecordService urlRecordService;

        public CategoryController(
            ICategoryService categoryService, 
            ILocalizationService localizationService,
            IUrlRecordService urlRecordService)
        {
            this.categoryService = categoryService;
            this.localizationService = localizationService;
            this.urlRecordService = urlRecordService;
        }

        private List<DaisyModels.Language> GetLanguages()
        {
            var languages = Mapper.Map<List<DaisyModels.Language>>(this.localizationService.GetLanguages().ToList());
            return languages;
        }

        public ActionResult Index()
        {
            var categories = categoryService.GetCategories();
            var model = Mapper.Map<List<DaisyModels.Category>>(categories);

            return View(model);
        }

        public ActionResult Create()
        {
            var languages = GetLanguages();
            var model = new DaisyModels.Category();
            model.Languages = languages;
            return View(model);
        }

        public ActionResult Edit(int id, bool photoActive = false)
        {
            var languages = GetLanguages();      
            var category = categoryService.GetCategoryBy(id);
            var photos = categoryService.GetCategoryPhotos(id);
            var slug = urlRecordService.GetActiveSlug(category.Id, typeof(DaisyEntities.Category).Name, category.LanguageId);

            var model = Mapper.Map<DaisyModels.Category>(category);
            model.Languages = languages;
            model.Slug = slug;
            model.Photos = Mapper.Map<List<DaisyModels.Photo>>(photos);

            if (photoActive)
            {
                TempData["PhotoActive"] = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DaisyModels.Category model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<DaisyEntities.Category>(model);
                categoryService.UpdateCategory(entity);

                if (model.Slug.IsNullOrEmpty())
                {
                    model.Slug = model.Name;
                }
                urlRecordService.SaveSlug<DaisyEntities.Category>(entity, model.Slug, model.LanguageId);

                TempData["message"] = "Update successfully";
                ModelState.Clear();
                return RedirectToAction("Edit", new { id = entity.Id });
            }
            var languages = GetLanguages();
            model.Languages = languages;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DaisyModels.Category model)
        {
            if (ModelState.IsValid)
            {
                var entity = categoryService.GetCategoryBy(model.Id);

                entity.LanguageId = model.LanguageId;
                entity.Name = model.Name;
                entity.Description = model.Description;   
                entity.IsPublished = model.IsPublished;                
                entity.MetaDescription = model.MetaDescription;
                entity.MetaKeywords = model.MetaKeywords;
                entity.MetaTitle = model.MetaTitle;

                categoryService.UpdateCategory(entity);
                if (model.Slug.IsNullOrEmpty())
                {
                    model.Slug = model.Name;
                }
                model.Slug = urlRecordService.SaveSlug<DaisyEntities.Category>(entity, model.Slug, model.LanguageId);
                TempData["message"] = "Update successfully";
                ModelState.Clear();
            }
            var languages = GetLanguages();
            model.Languages = languages;
            return View(model);
        }

        public ActionResult ViewPhotos()
        {
            return PartialView("_ViewPhotos");
        }

        [HttpPost]
        public ActionResult UpdatePhotos(int categoryId, int[] photoIds)
        {
            try
            {
                categoryService.AddCategoryPhotos(categoryId, photoIds);
                ViewBag.PhotoActive = true;
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }

        [HttpPost]
        public ActionResult DeletePhotos(int categoryId, int[] photoIds)
        {
            try
            {
                categoryService.DeleteCategoryPhotos(categoryId, photoIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }

        [HttpPost]
        public JsonResult UpdatePhotoOrder(int categoryId, int[] photoIds)
        {
            try
            {
                categoryService.UpdateCategoryPhotoOrder(categoryId, photoIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }
    }
}