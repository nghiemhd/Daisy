using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
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
            container.RegisterType<IDbContext, DataContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork<DataContext>>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IFlickrService, FlickrService>(new InjectionConstructor());
            //container.RegisterType<IFlickrService, FlickrService>(new InjectionConstructor(apiKey, sharedSecret));
            container.RegisterType<IAlbumService, AlbumService>();
        }
    }
}
