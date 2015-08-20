using Daisy.Core.Entities;
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

        Slider GetSliderBy(int id);

        Slider GetFirstSlider();

        void AddSliderPhotos(Slider slider, int[] photoIds);

        void DeleteSliderPhotos(Slider slider, int[] photoIds);
    }
}
