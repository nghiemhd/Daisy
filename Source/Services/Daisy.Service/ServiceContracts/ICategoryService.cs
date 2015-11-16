using Daisy.Core.Entities;
using Daisy.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface ICategoryService
    {
        IList<Category> GetCategories();

        Category GetCategoryBy(int id);

        IList<PublishedCategoryDto> GetPublishedCategories(int languageId);

        IList<Photo> GetCategoryPhotos(int categoryId);

        void UpdateCategory(Category entity);

        void AddCategoryPhotos(int categoryId, int[] photoIds);

        void DeleteCategoryPhotos(int categoryId, int[] photoIds);

        void UpdateCategoryPhotoOrder(int categoryId, int[] photoIds);
    }
}
