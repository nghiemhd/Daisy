using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Common
{
    public static class Constants
    {
        public static readonly string FlickrApiKey = "apiKey";
        public static readonly string FlickrSharedSecret = "sharedSecret";
        public static readonly string FlickrToken = "token";
        public static readonly string FlickrUserId = "userId";

        public static readonly int DefaultPageSize = 50;
        public static readonly int MaxPageSize = 500;
        public static readonly int MaxAlbumImport = 30;
    }
}
