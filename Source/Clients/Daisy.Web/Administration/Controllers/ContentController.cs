﻿using AutoMapper;
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
using Daisy.Logging.Extensions;

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
            var slider = contentService.GetFirstSlider();
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
        public ActionResult UpdateSlider(int[] photoIds)
        {
            try
            {
                if (photoIds.Length > Constants.MaxSliderPhotos)
                {
                    return Json("Cannot add more than 10 photos.");
                }

                var slider = contentService.GetFirstSlider();
                if (slider.Photos.Count + photoIds.Count() > Constants.MaxSliderPhotos)
                {
                    return Json("Slider cannot have more than 10 photos.");
                }

                contentService.AddSliderPhotos(slider, photoIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }            
        }

        [HttpPost]
        public ActionResult DeleteSliderPhotos(int sliderId, int[] photoIds)
        {
            try
            {
                var slider = contentService.GetSliderBy(sliderId);
                contentService.DeleteSliderPhotos(slider, photoIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {                
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }
    }
}