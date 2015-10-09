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

        public CategoryService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.categoryRepository = this.unitOfWork.GetRepository<Category>();
            this.categoryPhotoRepository = this.unitOfWork.GetRepository<CategoryPhoto>();
        }

        public List<Category> GetCategories()
        {
            var categories = Process(() =>
            {
                return this.categoryRepository.Query().ToList();
            });

            return categories;
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
    }
}
