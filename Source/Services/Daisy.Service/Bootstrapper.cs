using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Security;
using Daisy.Service.ServiceContracts;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class Bootstrapper
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            var connection = ConfigurationManager.ConnectionStrings["DataContext"];
            var connectionString = connection.ConnectionString;
            if (Encryption.IsEncrypt(connectionString))
            {
                connectionString = Encryption.Decrypt(connectionString);
            }

            container.RegisterType<IDbContext, DataContext>(new InjectionConstructor(connectionString));
            container.RegisterType<IUnitOfWork, UnitOfWork<DataContext>>();
            container.RegisterType<ILogger, Logger>(new InjectionConstructor("DaisyWeb"));
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IFlickrService, FlickrService>(new InjectionConstructor());
            container.RegisterType<IAlbumService, AlbumService>();
            container.RegisterType<IPhotoService, PhotoService>();
            container.RegisterType<IUploadService, UploadService>();
            container.RegisterType<IContentService, ContentService>();
            container.RegisterType<ILocalizationService, LocalizationService>();
        }
    }
}
