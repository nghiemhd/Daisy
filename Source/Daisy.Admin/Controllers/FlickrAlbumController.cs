using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daisy.Admin.Models;
using Daisy.Service.DataContracts;

namespace Daisy.Admin.Controllers
{
    public class FlickrAlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly string flickrUserId;

        public FlickrAlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
            this.flickrUserId = ConfigurationManager.AppSettings[Constants.FlickrUserId];
        }
        
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Search(SearchAlbumModel options)
        {
            try
            {
                if (options.UserId.IsNullOrEmpty())
                {
                    options.UserId = flickrUserId;
                }
                var searchOptions = Mapper.Map<SearchAlbumOptions>(options);
                var albums = albumService.GetAlbumsFromFlickr(searchOptions);
                var mappingAlbums = Mapper.Map<PagedList<DaisyModels.Album>>(albums);
                var result = new PagedListAlbumViewModel
                {
                    Albums = mappingAlbums,
                    SearchOptions = options
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Detail(string id)
        {
            var photos = albumService.GetPhotosByAlbumFromFlickr(id);

            var mappingPhotos = Mapper.Map<List<DaisyModels.Photo>>(photos);
            var model = new DaisyModels.AlbumViewModel
            {
                Photos = mappingPhotos
            };
            return View(model);
        }
    }
}