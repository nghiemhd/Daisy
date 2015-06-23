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

        public FlickrAlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }
        
        public ActionResult Search()                
        {
            return View();
        }

        public ActionResult AjaxSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(PagedListAlbumViewModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }

                var searchOptions = Mapper.Map<SearchAlbumOptions>(model.SearchOptions);
                var albums = albumService.GetAlbumsFromFlickr(searchOptions);
                var mappingAlbums = Mapper.Map<List<Album>>(albums.Items);
                var pagedListAlbums = new PagedList<Album>(mappingAlbums, albums.PageIndex, albums.PageSize, albums.TotalCount);
                model.Albums = pagedListAlbums;

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
                if (options == null)
                {
                    throw new ArgumentNullException("options");
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