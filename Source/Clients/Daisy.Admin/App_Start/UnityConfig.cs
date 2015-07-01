using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Daisy.Service.ServiceContracts;
using Daisy.Service;
using Daisy.Core.Infrastructure;
using System.Configuration;
using Daisy.Common;

namespace Daisy.Admin.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
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

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            //var apiKey = ConfigurationManager.AppSettings[Constants.FlickrApiKey];
            //var sharedSecret = ConfigurationManager.AppSettings[Constants.FlickrSharedSecret];

            // TODO: Register your types here
            container.RegisterType<IDbContext, DataContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork<DataContext>>();
            container.RegisterType<IFlickrService, FlickrService>(new InjectionConstructor());
            //container.RegisterType<IFlickrService, FlickrService>(new InjectionConstructor(apiKey, sharedSecret));
            container.RegisterType<IAlbumService, AlbumService>();
        }
    }
}
