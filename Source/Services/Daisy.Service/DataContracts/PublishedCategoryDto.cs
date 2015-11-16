using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class PublishedCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PageSize { get; set; }

        public bool IsPublish { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        public string Slug { get; set; }
    }
}
