using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Daisy.Common;
using Daisy.Logging.Extensions;
using Daisy.Service.Common;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;

using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Admin.Models;

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
        
        public ActionResult Index()                
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
                var albums = albumService.SearchFlickrAlbums(searchOptions);

                var albumsModel = Mapper.Map<List<DaisyModels.Album>>(albums.Items);
                var pagedListAlbums = new PagedList<DaisyModels.Album>(albumsModel, albums.PageIndex, albums.PageSize, albums.TotalCount);
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
            var album = albumService.GetFlickrAlbumById(id);
            var albumModel = Mapper.Map<DaisyModels.Album>(album);
            var photosModel = Mapper.Map<List<DaisyModels.Photo>>(photos);
            var model = new DaisyModels.AlbumDetailViewModel
            {
                Album = albumModel,
                Photos = photosModel
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ImportAlbumDetail(DaisyModels.AlbumDetailViewModel albumDetail)
        {
            try
            {
                var album = Mapper.Map<AlbumDetailDto>(albumDetail);

                albumService.ImportAlbumDetail(album);

                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }            
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
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }            
        }
    }
}