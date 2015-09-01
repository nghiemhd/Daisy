using AutoMapper;
using Daisy.Common;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Admin.Models;
using Daisy.Service.DataContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Daisy.Admin.Infrastructure.AutoMapperConfig), "RegisterMappings")]

namespace Daisy.Admin.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<FlickrNet.Photoset, DaisyModels.Album>()
                .ForMember(dest => dest.FlickrAlbumId, opt => opt.MapFrom(src => src.PhotosetId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.PrimaryPhoto.Medium640Url));

            Mapper.CreateMap<FlickrNet.Photo, DaisyModels.Photo>()
                .ForMember(dest => dest.FlickrPhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

            Mapper.CreateMap<DaisyModels.SearchAlbumModel, SearchAlbumOptions>();

            Mapper.CreateMap<DaisyModels.SearchPhotoModel, SearchPhotoOptions>();

            Mapper.CreateMap<DaisyModels.Album, DaisyEntities.Album>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.AlbumThumbnailUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<DaisyModels.Album, AlbumDto>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.AlbumThumbnailUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<AlbumDto, DaisyEntities.Album>();

            Mapper.CreateMap<FlickrNet.Photo, DaisyEntities.Photo>()
                .ForMember(dest => dest.FlickrPhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<DaisyModels.Photo, DaisyEntities.Photo>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<DaisyModels.Photo, PhotoDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<PhotoDto, DaisyEntities.Photo>();

            Mapper.CreateMap<DaisyModels.AlbumDetailViewModel, AlbumDetailDto>();

            Mapper.CreateMap<DaisyEntities.Album, DaisyModels.Album>()
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl));

            Mapper.CreateMap<DaisyEntities.Photo, DaisyModels.Photo>();

            Mapper.CreateMap<DaisyEntities.Slider, DaisyModels.SliderViewModel>();

            Mapper.CreateMap<DaisyModels.Blog, DaisyEntities.Blog>();

            Mapper.CreateMap<DaisyEntities.Blog, DaisyModels.Blog>()
                .ForMember(dest => dest.Content, opt => opt.ResolveUsing(src =>
                {
                    if (src.Content.Length > 50)
                    {
                        return src.Content.Substring(0, 50) + "...";
                    }
                    else
                    {
                        return src.Content;
                    }
                }));

            Mapper.CreateMap<DaisyModels.SearchBlogModel, SearchBlogOptions>();
        }
    }
}