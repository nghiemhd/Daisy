using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.UnitTest.MockObjects
{
    public class MockDataContext
    {
        public IList<Photo> Photo
        {
            get
            {
                return new List<Photo>
                {
                    new Photo { Id = 1, Name = "Photo 1", Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
                    new Photo { Id = 2, Name = "Photo 2", Url = "https://c4.staticflickr.com/8/7650/16830021960_62da296e47_h.jpg", IsDisplayed = true },
                };
            }
        }
    }
}
