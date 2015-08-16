using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class SliderViewModel
    {
        public SliderViewModel()
        {
            Photos = new List<Photo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Page { get; set; }

        public int Order { get; set; }

        public IList<Photo> Photos { get; set; }
    }
}