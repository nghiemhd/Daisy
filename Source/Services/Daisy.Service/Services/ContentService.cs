using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.ServiceContracts;
using System.Collections.Generic;
using System.Linq;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service
{
    public class ContentService : HandleErrorService, IContentService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Slider> sliderRepository;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private IRepository<DaisyEntities.SliderPhoto> sliderPhotoRepository;
        private ILogger logger;

        public ContentService(IUnitOfWork unitOfWork, ILogger logger)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sliderRepository = this.unitOfWork.GetRepository<DaisyEntities.Slider>();
            this.photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
            this.sliderPhotoRepository = this.unitOfWork.GetRepository<DaisyEntities.SliderPhoto>();
        }

        //public void UpdateSlider(SliderDto sliderDto)
        //{
        //    if (sliderDto.Id > 0)
        //    {                 
        //        var slider = sliderRepository.Query().Where(x => x.Id == sliderDto.Id).FirstOrDefault();
        //        if (slider != null)
        //        {
        //            var sql = "DELETE SliderPhoto WHERE SliderId = @SliderId";
        //            this.unitOfWork.DbContext.ExecuteSqlCommand(sql, new SqlParameter("@SliderId", sliderDto.Id));

        //            var photos = photoRepository.Query().Where(x => sliderDto.PhotoIds.Contains(x.Id)).ToList();
        //            slider.Photos = photos;
        //            unitOfWork.Commit();
        //        }
        //    }            
        //}

        public DaisyEntities.Slider GetSliderBy(int id)
        {
            var slider = Process(() =>
            {
                return sliderRepository.Query().Where(x => x.Id == id).FirstOrDefault();
            });
            return slider;
        }

        public DaisyEntities.Slider GetFirstSlider()
        {
            var slider = Process(() =>
            {
                return sliderRepository.Query().FirstOrDefault();
            });
            return slider;
        }

        public void AddSliderPhotos(DaisyEntities.Slider slider, int[] photoIds)
        {
            Process(() =>
            {
                foreach (var photoId in photoIds)
                {
                    var sliderPhoto = new DaisyEntities.SliderPhoto
                    {
                        SliderId = slider.Id,
                        PhotoId = photoId
                    };
                    sliderPhotoRepository.Insert(sliderPhoto);
                }

                this.unitOfWork.Commit();
            });            
        }

        public void DeleteSliderPhotos(DaisyEntities.Slider slider, int[] photoIds)
        {
            Process(() =>
            {
                var photosToDelete = sliderPhotoRepository.Query().Where(x => photoIds.Contains(x.PhotoId)).ToList();
                sliderPhotoRepository.RemoveRange(photosToDelete);
                this.unitOfWork.Commit();
            });            
        }

        public List<DaisyEntities.Photo> GetPhotosOfSlider(int sliderId)
        {
            var result = Process(() =>
            {
                var query = from sp in sliderPhotoRepository.Query()
                            join p in photoRepository.Query()
                            on sp.PhotoId equals p.Id
                            where sp.SliderId == sliderId
                            orderby sp.DisplayOrder
                            select p;

                return query.ToList();
            });

            return result;
        }

        public void UpdateSliderPhotoOrder(int sliderId, int[] photoIds)
        {
            Process(() =>
            {
                var minOrder = sliderPhotoRepository.Query()
                    .Where(x => x.SliderId == sliderId && photoIds.Contains(x.PhotoId))
                    .OrderBy(x => x.DisplayOrder)
                    .Select(x => x.DisplayOrder)
                    .FirstOrDefault();

                foreach (var photoId in photoIds)
                {
                    var sliderPhoto = sliderPhotoRepository.Query()
                        .Where(x => x.SliderId == sliderId && x.PhotoId == photoId).First();

                    sliderPhoto.DisplayOrder = minOrder;
                    minOrder++;
                }

                this.unitOfWork.Commit();
            });
        }
    }
}
