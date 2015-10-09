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

        void UpdateCategory(Category entity);
    }
}
