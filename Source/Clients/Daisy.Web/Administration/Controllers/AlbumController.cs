using AutoMapper;
using Daisy.Common;
using Daisy.Logging.Extensions;
using Daisy.Service.Common;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using DaisyModels = Daisy.Admin.Models;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService)
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
                var albums = albumService.SearchAlbums(searchOptions);

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

        public ActionResult Edit(int id)
        {           
            var album = albumService.GetAlbumById(id);
            var albumModel = Mapper.Map<DaisyModels.Album>(album);
            var photosModel = Mapper.Map<List<DaisyModels.Photo>>(album.Photos);
            var model = new DaisyModels.AlbumDetailViewModel
            {
                Album = albumModel,
                Photos = photosModel
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult Publish(int[] albumIds, bool isPublished)
        {
            try
            {
                albumService.PublishAlbums(albumIds, isPublished);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }         
        }

        [HttpPost]
        public JsonResult PublishPhotos(int albumId, int[] photoIds, bool isPublished)
        {
            try
            {
                albumService.PublishPhotos(albumId, photoIds, isPublished);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }

        [HttpPost]
        public JsonResult DeleteAlbums(int[] albumIds)
        {
            try
            {
                albumService.DeleteAlbums(albumIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }

        [HttpPost]
        public JsonResult UpdateAlbumOrder(int[] albumIds)
        {
            try
            {
                albumService.UpdateAlbumOrder(albumIds);
                return Json(ResponseStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                return Json(LogExtension.GetFinalInnerException(ex).Message);
            }
        }
    }
}