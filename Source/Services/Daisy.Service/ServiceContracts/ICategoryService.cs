using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface ICategoryService
    {
        List<Category> GetCategories();

        Category GetCategoryBy(int id);

        List<Photo> GetCategoryPhotos(int categoryId);

        void UpdateCategory(Category entity);

        void AddCategoryPhotos(int categoryId, int[] photoIds);

        void DeleteCategoryPhotos(int categoryId, int[] photoIds);

        void UpdateCategoryPhotoOrder(int categoryId, int[] photoIds);
    }
}
