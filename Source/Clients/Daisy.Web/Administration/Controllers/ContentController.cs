using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daisy.Service.DataContracts;
using Daisy.Common;
using Daisy.Service.Common;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IContentService contentService;
        private readonly IPhotoService photoService;

        public ContentController(IContentService contentService, IPhotoService photoService)
        {
            this.contentService = contentService;
            this.photoService = photoService;
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

        public ActionResult ViewPhotos()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchPhotos(DaisyModels.SearchPhotoModel options)
        {
            try
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }
                var searchOptions = Mapper.Map<SearchPhotoOptions>(options);
                var photos = photoService.SearchPhotos(searchOptions);

                var photosModel = Mapper.Map<List<DaisyModels.Photo>>(photos.Items);
                var pagedListPhotos = new PagedList<DaisyModels.Photo>(photosModel, photos.PageIndex, photos.PageSize, photos.TotalCount);
                var result = new DaisyModels.PagedListPhotoViewModel
                {
                    Photos = pagedListPhotos,
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
        public JsonResult UpdateSlider(int[] photoIds)
        {
            return Json(ResponseStatus.Success.ToString());
        }
    }
}