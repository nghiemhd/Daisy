using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service
{
    public class ContentService : IContentService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Slider> sliderRepository;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private ILogger logger;

        public ContentService(IUnitOfWork unitOfWork, ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sliderRepository = this.unitOfWork.GetRepository<DaisyEntities.Slider>();
            this.photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
        }

        public void UpdateSlider(SliderDto sliderDto)
        {
            if (sliderDto.Id > 0)
            {                 
                var slider = sliderRepository.Query().Where(x => x.Id == sliderDto.Id).FirstOrDefault();
                if (slider != null)
                {
                    var sql = "DELETE SliderPhoto WHERE SliderId = @SliderId";
                    this.unitOfWork.DbContext.ExecuteSqlCommand(sql, new SqlParameter("@SliderId", sliderDto.Id));

                    var photos = photoRepository.Query().Where(x => sliderDto.PhotoIds.Contains(x.Id)).ToList();
                    slider.Photos = photos;
                    unitOfWork.Commit();
                }
            }
            
        }

        public DaisyEntities.Slider GetSliderBy(int id)
        {
            return sliderRepository.Query().FirstOrDefault();
        }
    }
}
