using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class CategoryService : HandleErrorService, ICategoryService
    {
        private IUnitOfWork unitOfWork;
        private ILogger logger;
        private IRepository<Category> categoryRepository;
        private IRepository<CategoryPhoto> categoryPhotoRepository;
        private IRepository<Photo> photoRepository;

        public CategoryService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.categoryRepository = this.unitOfWork.GetRepository<Category>();
            this.categoryPhotoRepository = this.unitOfWork.GetRepository<CategoryPhoto>();
            this.photoRepository = this.unitOfWork.GetRepository<Photo>();
        }

        public List<Category> GetCategories()
        {
            var categories = Process(() =>
            {
                return this.categoryRepository.Query().ToList();
            });

            return categories;
        }

        public Category GetCategoryBy(int id)
        {
            var category = Process(() =>
            {
                return categoryRepository.Query().Where(x => x.Id == id).FirstOrDefault();
            });

            return category;
        }

        public List<Photo> GetCategoryPhotos(int categoryId)
        {
            var result = Process(() =>
            {
                var query = from cp in categoryPhotoRepository.Query()
                            join p in photoRepository.Query()
                            on cp.PhotoId equals p.Id
                            where cp.CategoryId == categoryId
                            orderby cp.DisplayOrder
                            select p;

                return query.ToList();
            });

            return result;
        }

        public void UpdateCategory(Category entity)
        {
            Process(() =>
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (entity.Id <= 0)
                {
                    this.categoryRepository.Insert(entity);
                }
                else
                {
                    this.categoryRepository.Update(entity);
                }

                this.unitOfWork.Commit();
            });
        }

        public void AddCategoryPhotos(int categoryId, int[] photoIds)
        {
            Process(() =>
            {
                var photosInDb = categoryPhotoRepository
                    .Query()
                    .Where(x => x.CategoryId == categoryId)
                    .Select(x => x.PhotoId)
                    .ToList();

                var newPhotos = photoIds.Where(x => !photosInDb.Contains(x)).Distinct().ToList();
                var maxOrder = categoryPhotoRepository
                    .Query()
                    .Where(x => x.CategoryId == categoryId)
                    .OrderByDescending(x => x.DisplayOrder)
                    .Select(x => x.DisplayOrder)
                    .FirstOrDefault();

                foreach (var photoId in newPhotos)
                {
                    maxOrder++;
                    var categoryPhoto = new CategoryPhoto
                    {
                        CategoryId = categoryId,
                        PhotoId = photoId,
                        DisplayOrder = maxOrder
                    };
                    categoryPhotoRepository.Insert(categoryPhoto);                    
                }

                this.unitOfWork.Commit();
            }); 
        }

        public void DeleteCategoryPhotos(int categoryId, int[] photoIds)
        {
            Process(() =>
            {
                var photosToDelete = categoryPhotoRepository
                    .Query()
                    .Where(x => photoIds.Contains(x.PhotoId) && x.CategoryId == categoryId)
                    .ToList();
                categoryPhotoRepository.RemoveRange(photosToDelete);
                this.unitOfWork.Commit();
            }); 
        }

        public void UpdateCategoryPhotoOrder(int categoryId, int[] photoIds)
        {
            Process(() =>
            {
                var minOrder = categoryPhotoRepository
                    .Query()
                    .Where(x => x.CategoryId == categoryId && photoIds.Contains(x.PhotoId))
                    .OrderBy(x => x.DisplayOrder)
                    .Select(x => x.DisplayOrder)
                    .FirstOrDefault();

                foreach (var photoId in photoIds)
                {
                    var categoryPhoto = categoryPhotoRepository.Query()
                        .Where(x => x.CategoryId == categoryId && x.PhotoId == photoId).First();

                    categoryPhoto.DisplayOrder = minOrder;
                    minOrder++;
                }

                this.unitOfWork.Commit();
            });
        }        
    }
}
