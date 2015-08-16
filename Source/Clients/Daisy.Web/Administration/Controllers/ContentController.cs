using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IContentService contentService;

        public ContentController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public ActionResult Slider()
        {
            var slider = contentService.GetSliderBy(1);
            if (slider == null)
            {
                slider = new DaisyEntities.Slider();
            }

            var model = Mapper.Map<DaisyModels.SliderViewModel>(slider);

            return View(model);
        }

        public ActionResult ViewAlbums()
        {
            return PartialView("_Album");
        }
    }
}