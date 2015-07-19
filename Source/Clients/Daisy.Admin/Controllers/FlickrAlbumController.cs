using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using Daisy.Common;
using Daisy.Common.Extensions;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using Daisy.Service.Common;

namespace Daisy.Admin.Controllers
{
    [Authorize]
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

        [HttpPost]
        public JsonResult Search(DaisyModels.SearchAlbumModel options)
        {
            try
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }
                var searchOptions = Mapper.Map<SearchAlbumOptions>(options);
                var albums = albumService.GetFlickrAlbums(searchOptions);
                var mappingAlbums = Mapper.Map<List<DaisyModels.Album>>(albums.Items);
                var pagedListAlbums = new PagedList<DaisyModels.Album>(mappingAlbums, albums.PageIndex, albums.PageSize, albums.TotalCount);
                var result = new DaisyModels.PagedListAlbumViewModel
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

        public ActionResult Edit(string id)
        {
            var photos = albumService.GetPhotosByFlickrAlbum(id);

            var mappingPhotos = Mapper.Map<List<DaisyModels.Photo>>(photos);
            var model = new DaisyModels.AlbumDetailViewModel
            {
                Photos = mappingPhotos
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(IEnumerable<DaisyModels.Album> albums)
        {
            try
            {
                var entities = Mapper.Map<IEnumerable<DaisyEntities.Album>>(albums);
                if (albums.Count() > Constants.MaxAlbumImport)
                {
                    return Json(ResponseStatus.OutOfRange.ToString());
                }
                albumService.ImportAlbums(entities);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}