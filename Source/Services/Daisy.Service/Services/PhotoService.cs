using Daisy.Common;
using Daisy.Common.Extensions;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daisy.Service.DataContracts;
using Daisy.Logging;

namespace Daisy.Service
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private ILogger logger;    
        private IFlickrService flickrService;

        public PhotoService(IUnitOfWork unitOfWork, ILogger logger, IFlickrService flickrService)
        {
            this.unitOfWork = unitOfWork;
            photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
            this.logger = logger;
            this.flickrService = flickrService;
        }

        public IEnumerable<DaisyEntities.Photo> GetAllPhotos()
        {
            return photoRepository.Query().ToList();
        }

        public IEnumerable<DaisyEntities.Photo> GetDisplayedPhotos()
        {
            return photoRepository.Query().Where(x => x.IsPublished == true).ToList();
        }

        public DaisyEntities.Photo GetPhotoById(int id)
        {
            var photo = photoRepository
                .Query()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return photo;
        }

        public void DeletePhotoById(int id)
        {
            var photo = GetPhotoById(id);
            photoRepository.Delete(photo);
            unitOfWork.Commit();
        }

        public void DeletePhotos(IEnumerable<DaisyEntities.Photo> photos)
        {
            throw new NotImplementedException();
        }

        public FlickrNet.PhotoInfo GetFlickrPhotoInfo(string id)
        {
            return flickrService.GetPhotoInfo(id);
        }

        public PagedList<DaisyEntities.Photo> SearchPhotos(SearchPhotoOptions options)
        {
            try
            {
                var query = photoRepository.Query();
                if (!options.AlbumName.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Albums.Any(album => album.Name.Contains(options.AlbumName)));
                }

                if (options.IsPublished != null)
                {
                    query = query.Where(x => x.IsPublished == options.IsPublished);
                }

                if (options.PageSize <= 0 || options.PageSize > Constants.MaxPageSize)
                {
                    options.PageSize = Constants.DefaultPageSize;
                }

                if (options.PageIndex < 0)
                {
                    options.PageIndex = 0;
                }

                int totalCount = query.Count();
                query = query
                        .OrderBy(x => x.Id)
                        .Skip(options.PageSize * options.PageIndex)
                        .Take(options.PageSize);

                var result = new PagedList<DaisyEntities.Photo>(
                    query.ToList(),
                    options.PageIndex,
                    options.PageSize,
                    totalCount
                );

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
