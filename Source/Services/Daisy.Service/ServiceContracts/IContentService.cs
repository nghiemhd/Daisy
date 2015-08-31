﻿using Daisy.Common;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface IContentService
    {
        void UpdateSlider(SliderDto slider);

        DaisyEntities.Slider GetSliderBy(int id);

        DaisyEntities.Slider GetFirstSlider();

        void AddSliderPhotos(DaisyEntities.Slider slider, int[] photoIds);

        void DeleteSliderPhotos(DaisyEntities.Slider slider, int[] photoIds);

        void UpdateBlog(DaisyEntities.Blog blog);

        PagedList<DaisyEntities.Blog> SearchBlogs(SearchBlogOptions options);
    }
}
