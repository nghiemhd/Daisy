using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LanguageCulture { get; set; }

        public string UniqueSeoCode { get; set; }

        public string FlagImageFileName { get; set; }

        public bool Rtl { get; set; }

        public bool IsPublished { get; set; }

        public int DisplayOrder { get; set; }
    }
}
