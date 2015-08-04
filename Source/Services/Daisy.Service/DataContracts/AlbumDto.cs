using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class AlbumDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool IsDisplayed { get; set; }

        public string FlickrAlbumId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
