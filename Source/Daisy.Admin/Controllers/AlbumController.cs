using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using Daisy.Common;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Admin.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly string flickrUserId;

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
            this.flickrUserId = ConfigurationManager.AppSettings[Constants.FlickrUserId];
        }
        
        public ActionResult Index()
        {
            var albums = albumService.GetAllAlbumsFromFlickr(flickrUserId);

            var mappingAlbums = Mapper.Map<List<DaisyModels.Album>>(albums);
            var model = new DaisyModels.AlbumListViewModel 
            { 
                Albums = mappingAlbums
            };
            return View(model);
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