using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using Daisy.Service;
using Daisy.Core.Entities;
using Daisy.Security;

namespace Daisy.IntegrationTest
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            //container = new UnityContainer();
            //container.RegisterType<IDbContext, DataContext>();
            //container.RegisterType<IUnitOfWork, UnitOfWork<DataContext>>();
            //container.RegisterType<IAuthenticationService, AuthenticationService>();
            container = Bootstrapper.GetConfiguredContainer();
        }

        [TestMethod]
        public void TestRegisterUser()
        {
            //var user = new User 
            //{ 
            //    Username = "daisyadmin",
            //    Password = "daisy@123"
            //};

            var user = new User
            {
                Username = "nghiemhd",
                Password = "nghiemhd"
            };

            var authenticationService = container.Resolve<IAuthenticationService>();
            authenticationService.RegisterUser(user);
            var result = authenticationService.GetUserByUsername(user.Username);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod]
        public void TestValidateUser()
        { 
            var authenticationService = container.Resolve<IAuthenticationService>();
            var isValid = authenticationService.ValidateUser("daisyadmin", "daisy@123");

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Decrypt()
        {
            var str = "8et3iY2DvXJN3sUafdoh6Y2DlPxL7navikUFTuZQ0kd3LGvSW2rmAXRvEovxopyelnoLcHTGTOW5yj3mVbE1zgC08gqoHYLDi1Xf3GHILQpGPSLiLdb1cBnNd76oXNNmZeFzzdqCylbz6xCEGILEn0F5FZPsiykfwsfsgyGze1SQxkVSCSQMqwVGFb9CaUzDKn+3u1Pygp9dlEcFNGNp9No7xiHjraYKumoP67aBiSE=#####";
            var result = Encryption.Decrypt(str);
        }
    }
}
