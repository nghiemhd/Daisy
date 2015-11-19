using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.DataContracts;
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
        private IRepository<UrlRecord> urlRecordRepository;
        private IRepository<Language> languageRepository;

        public CategoryService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.categoryRepository = this.unitOfWork.GetRepository<Category>();
            this.categoryPhotoRepository = this.unitOfWork.GetRepository<CategoryPhoto>();
            this.photoRepository = this.unitOfWork.GetRepository<Photo>();
            this.urlRecordRepository = this.unitOfWork.GetRepository<UrlRecord>();
            this.languageRepository = this.unitOfWork.GetRepository<Language>();
        }

        public IList<Category> GetCategories()
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

        public IList<PublishedCategoryDto> GetPublishedCategories(int languageId)
        {
            var categories = Process(() =>
            {
                var query = from c in categoryRepository.Query()
                            join l in languageRepository.Query()
                            on c.LanguageId equals l.Id
                            join u in urlRecordRepository.Query().Where(x => x.EntityName == typeof(Category).Name)
                            on new { p1 = c.Id, p2 = c.LanguageId } equals new { p1 = u.EntityId, p2 = u.LanguageId }
                            where c.IsPublished == true
                            select new PublishedCategoryDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                                PageSize = c.PageSize,
                                LanguageId = c.LanguageId,
                                Language = l.Name,
                                IsPublished = c.IsPublished,
                                Slug = u.Slug
                            };
                return query.ToList();
            });

            return categories;
        }

        public IList<PublishedCategoryDto> GetCategoryDtos()
        {
            var categories = Process(() =>
            {
                var query = from c in categoryRepository.Query()
                            join l in languageRepository.Query()
                            on c.LanguageId equals l.Id
                            join u in urlRecordRepository.Query().Where(x => x.EntityName == typeof(Category).Name)
                            on new { p1 = c.Id, p2 = c.LanguageId } equals new { p1 = u.EntityId, p2 = u.LanguageId }
                            select new PublishedCategoryDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                                PageSize = c.PageSize,
                                LanguageId = c.LanguageId,
                                Language = l.Name,
                                IsPublished = c.IsPublished,
                                Slug = u.Slug
                            };
                return query.ToList();
            });

            return categories;
        }

        public IList<Photo> GetCategoryPhotos(int categoryId)
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
