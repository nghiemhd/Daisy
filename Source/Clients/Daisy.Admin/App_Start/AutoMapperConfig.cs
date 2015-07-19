﻿using AutoMapper;
using Daisy.Common;
using DaisyEntities = Daisy.Core.Entities;
using DaisyModels = Daisy.Admin.Models;
using Daisy.Service.DataContracts;
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
                .ForMember(dest => dest.AlbumThumbnailUrl, opt => opt.MapFrom(src => src.PrimaryPhoto.Medium640Url));

            Mapper.CreateMap<FlickrNet.Photo, DaisyModels.Photo>()
                .ForMember(dest => dest.FlickrPhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

            Mapper.CreateMap<DaisyModels.SearchAlbumModel, SearchAlbumOptions>();

            Mapper.CreateMap<DaisyModels.Album, DaisyEntities.Album>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.AlbumThumbnailUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom( o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            Mapper.CreateMap<FlickrNet.Photo, DaisyEntities.Photo>()
                .ForMember(dest => dest.FlickrPhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SmallUrl, opt => opt.MapFrom(src => src.SmallUrl))
                .ForMember(dest => dest.MediumUrl, opt => opt.MapFrom(src => src.MediumUrl))
                .ForMember(dest => dest.LargeUrl, opt => opt.MapFrom(src => src.LargeUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(o => HttpContext.Current.User.Identity.Name));

            //Mapper.CreateMap<PagedList<FlickrNet.Photoset>, PagedList<DaisyModels.Album>>()
            //    .ConstructUsing(x => new PagedList<DaisyModels.Album>(x.))
        }
    }
}