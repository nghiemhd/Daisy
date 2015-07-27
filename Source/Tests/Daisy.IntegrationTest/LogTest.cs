using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Daisy.Service;
using Daisy.Logging;

namespace Daisy.IntegrationTest
{
    [TestClass]
    public class LogTest
    {
        IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            container = Bootstrapper.GetConfiguredContainer();
        }

        [TestMethod]
        public void TestLogError()
        {
            var logger = container.Resolve<ILogger>(new ResolverOverride[] { 
                new ParameterOverride("loggerName", "TestLogger") 
            });

            try
            {
                int a = 0;
                int b = 1;
                var c = b / a;
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                logger.Error(ex);
                throw;
            }
        }
    }
}
