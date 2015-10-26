using DaisyModels = Daisy.Admin.Models;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = categoryService.GetCategories();
            var model = Mapper.Map<List<DaisyModels.Category>>(categories);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DaisyModels.Category model)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<DaisyEntities.Category>(model);
                categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult ViewPhotos()
        {
            return PartialView("_ViewPhotos");
        }

        [HttpPost]
        public ActionResult UpdateCategoryPhotos(int[] photoIds)
        {
            return null;
        }
    }
}