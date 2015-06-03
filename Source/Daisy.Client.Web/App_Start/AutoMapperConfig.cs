using AutoMapper;
using Daisy.Client.Web.Models;
using Entities = Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Client.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Entities.Photo, Photo>();
        }
    }
}