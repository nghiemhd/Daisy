﻿using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.ServiceContracts;
using System.Linq;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service
{
    public class ContentService : HandleErrorService, IContentService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Slider> sliderRepository;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private ILogger logger;

        public ContentService(IUnitOfWork unitOfWork, ILogger logger)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sliderRepository = this.unitOfWork.GetRepository<DaisyEntities.Slider>();
            this.photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
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
            return slider as DaisyEntities.Slider;
        }

        public DaisyEntities.Slider GetFirstSlider()
        {
            var slider = Process(() =>
            {
                return sliderRepository.Query().FirstOrDefault();
            });
            return slider as DaisyEntities.Slider;
        }

        public void AddSliderPhotos(DaisyEntities.Slider slider, int[] photoIds)
        {
            Process(() =>
            {
                var photos = photoRepository.Query().Where(x => photoIds.Contains(x.Id)).ToList();
                foreach (var photo in photos)
                {
                    slider.Photos.Add(photo);
                }

                this.unitOfWork.Commit();
            });            
        }

        public void DeleteSliderPhotos(DaisyEntities.Slider slider, int[] photoIds)
        {
            Process(() =>
            {
                var photosToDelete = photoRepository.Query().Where(x => photoIds.Contains(x.Id)).ToList();
                foreach (var photo in photosToDelete)
                {
                    slider.Photos.Remove(photo);
                }
                this.unitOfWork.Commit();
            });            
        }        
    }
}
