using AutoMapper;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Daisy.Service.DataContracts;

namespace Daisy.Web.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<DaisyEntities.Album, DaisyModels.Album>()
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl));

            Mapper.CreateMap<DaisyEntities.Photo, DaisyModels.Photo>();

            Mapper.CreateMap<DaisyEntities.BlogPost, DaisyModels.Blog>();

            Mapper.CreateMap<PublishedCategoryDto, DaisyModels.Category>();

            Mapper.CreateMap<DaisyEntities.Category, DaisyModels.Category>();
        }
    }
}