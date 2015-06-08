using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using Daisy.Service;
using Daisy.Core.Entities;

namespace Daisy.IntegrationTest
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            container = new UnityContainer();
            container.RegisterType<IDbContext, DataContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork<DataContext>>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
        }

        [TestMethod]
        public void TestRegisterUser()
        {
            var user = new User 
            { 
                Username = "nghiemhd2",
                Password = "123"
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
            var isValid = authenticationService.ValidateUser("nghiemhd", "123");

            Assert.IsTrue(isValid);
        }
    }
}
