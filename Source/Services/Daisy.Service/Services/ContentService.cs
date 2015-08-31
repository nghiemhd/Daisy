using Daisy.Common;
using Daisy.Common.Extensions;
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
    public class ContentService : HandleErrorService, IContentService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Slider> sliderRepository;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private IRepository<DaisyEntities.Blog> blogRepository;
        private ILogger logger;

        public ContentService(IUnitOfWork unitOfWork, ILogger logger)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sliderRepository = this.unitOfWork.GetRepository<DaisyEntities.Slider>();
            this.photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
            this.blogRepository = this.unitOfWork.GetRepository<DaisyEntities.Blog>();
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

        public void UpdateBlog(DaisyEntities.Blog blog)
        {
            Process(() =>
            {
                if (blog == null)
                {
                    throw new ArgumentNullException("blog");
                }

                if (blog.Id <= 0)
                {
                    blogRepository.Insert(blog);
                }
                else
                {
                    blogRepository.Update(blog);
                }

                this.unitOfWork.Commit();
            });
        }

        public Daisy.Common.PagedList<DaisyEntities.Blog> SearchBlogs(SearchBlogOptions options)
        {
            var query = blogRepository.Query();

            if (!options.Title.IsNullOrEmpty())
            {
                query = query.Where(x => x.Title.Contains(options.Title));
            }

            if (options.IsPublished != null)
            {
                query = query.Where(x => x.IsPublished == options.IsPublished);
            }

            if (options.FromCreatedDate != null)
            {
                query = query.Where(x => x.CreatedDate >= options.FromCreatedDate);
            }

            if (options.ToCreatedDate != null)
            {
                query = query.Where(x => x.CreatedDate <= options.ToCreatedDate);
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
                    .OrderByDescending(x => x.Id)
                    .Skip(options.PageSize * options.PageIndex)
                    .Take(options.PageSize);

            var result = new PagedList<DaisyEntities.Blog>(
                query.ToList(),
                options.PageIndex,
                options.PageSize,
                totalCount
            );

            return result;
        }
    }
}
