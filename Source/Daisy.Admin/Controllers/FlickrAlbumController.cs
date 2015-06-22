using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Daisy.Admin.Models;
using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;

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

        [HttpGet]
        public ActionResult Search(SearchAlbumModel options)
        {
            try
            {
                if (options.UserId.IsNullOrEmpty())
                {
                    options.UserId = flickrUserId;
                }
                var searchOptions = Mapper.Map<SearchAlbumOptions>(options);
                var albums = albumService.GetAlbumsFromFlickr(searchOptions);
                var mappingAlbums = Mapper.Map<List<Album>>(albums.Items);
                var pagedListAlbums = new PagedList<Album>(mappingAlbums, albums.PageIndex, albums.PageSize, albums.TotalCount);
                var model = new PagedListAlbumViewModel
                {
                    Albums = pagedListAlbums,
                    SearchOptions = options
                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult AjaxSearch(SearchAlbumModel options)
        {
            try
            {
                if (options.UserId.IsNullOrEmpty())
                {
                    options.UserId = flickrUserId;                    
                }
                var searchOptions = Mapper.Map<SearchAlbumOptions>(options);
                var albums = albumService.GetAlbumsFromFlickr(searchOptions);
                var mappingAlbums = Mapper.Map<List<Album>>(albums.Items);
                var pagedListAlbums = new PagedList<Album>(mappingAlbums, albums.PageIndex, albums.PageSize, albums.TotalCount);
                var result = new PagedListAlbumViewModel
                {
                    Albums = pagedListAlbums,
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

            var mappingPhotos = Mapper.Map<List<Photo>>(photos);
            var model = new AlbumViewModel
            {
                Photos = mappingPhotos
            };
            return View(model);
        }
    }
}