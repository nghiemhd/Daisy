using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Daisy.Service.ServiceContracts;
using DaisyModels = Daisy.Web.Models;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult CategoryTopMenu()
        {
            int languageId = 1;
            var categoriesDto = categoryService.GetPublishedCategories(languageId);
            var categories = Mapper.Map<List<DaisyModels.Category>>(categoriesDto);
            var cachedModel = new DaisyModels.CategoryTopMenuModel
            {
                Categories = categories
            };
            return PartialView("_CategoryTopMenu", cachedModel);
        }

        public ActionResult Detail(int id)
        {
            var category = categoryService.GetCategoryBy(id);
            var model = Mapper.Map<DaisyModels.Category>(category);
            var photos = categoryService.GetCategoryPhotos(id);
            model.Photos = Mapper.Map<List<DaisyModels.Photo>>(photos);

            return View(model);
        }
    }
}