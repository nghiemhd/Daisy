using AutoMapper;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using DaisyModels = Daisy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daisy.Common;

namespace Daisy.Web.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public ActionResult Index()
        {
            return Navigate("1");
        }

        [Route("album/page{pageNumber}")]
        public ActionResult Navigate(string pageNumber)
        {
            int pageIndex = 0;

            try
            {
                pageIndex = int.Parse(pageNumber) - 1;
                if (pageIndex < 0)
                {
                    return PartialView("PageNotFound");
                }
            }
            catch
            {
                return PartialView("PageNotFound");
            }

            var searchOptions = new SearchAlbumOptions
            {
                IsPublished = true,
                PageIndex = pageIndex,
                PageSize = 10
            };
            var albums = albumService.SearchAlbums(searchOptions);

            var albumsModel = Mapper.Map<List<DaisyModels.Album>>(albums.Items);
            var pagedListAlbums = new PagedList<DaisyModels.Album>(albumsModel, albums.PageIndex, albums.PageSize, albums.TotalCount);

            var model = new DaisyModels.PagedListAlbumViewModel { Albums = pagedListAlbums };

            return View("Index", model);
        }

        [Route("album/{albumId:int}")]
        public ActionResult Detail(int albumId)
        {
            var album = albumService.GetAlbumById(albumId);
            if (album == null || !album.IsPublished)
            {
                return PartialView("PageNotFound");
            }

            var model = new DaisyModels.AlbumDetailViewModel { 
                Album = Mapper.Map<DaisyModels.Album>(album),
                Photos = Mapper.Map<List<DaisyModels.Photo>>(album.Photos.Where(x => x.IsPublished))
            };

            return View(model);
        }
    }
}