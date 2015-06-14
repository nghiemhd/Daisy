using AutoMapper;
using DaisyModels = Daisy.Admin.Models;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<FlickrNet.Photoset, DaisyModels.Album>()
                .ForMember(dest => dest.FlickrAlbumId, opt => opt.MapFrom(src => src.PhotosetId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.PrimaryPhoto.Small320Url));

            Mapper.CreateMap<FlickrNet.Photo, DaisyModels.Photo>()
                .ForMember(dest => dest.FlickrPhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.LargeUrl));
        }
    }
}