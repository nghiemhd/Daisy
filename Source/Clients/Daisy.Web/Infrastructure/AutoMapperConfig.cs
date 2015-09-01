using AutoMapper;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<DaisyEntities.Album, DaisyModels.Album>()
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl));

            Mapper.CreateMap<DaisyEntities.Photo, DaisyModels.Photo>();

            Mapper.CreateMap<DaisyEntities.Blog, DaisyModels.Blog>()
                .ForMember(dest => dest.Content, opt => opt.ResolveUsing(src =>
                {
                    if (src.Content.Length > 500)
                    {
                        return src.Content.Substring(0, 500) + "...";
                    }
                    else
                    {
                        return src.Content;
                    }
                }));
        }
    }
}