using AutoMapper;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<DaisyEntities.Album, DaisyModels.Album>()
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl));

            Mapper.CreateMap<DaisyEntities.Photo, DaisyModels.Photo>();
        }
    }
}