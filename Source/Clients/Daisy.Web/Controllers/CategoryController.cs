using AutoMapper;
using Daisy.Service.ServiceContracts;
using Daisy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var categories = Mapper.Map<List<Category>>(categoriesDto);
            var cachedModel = new CategoryTopMenuModel
            {
                Categories = categories
            };
            return PartialView("_CategoryTopMenu", cachedModel);
        }
    }
}