using Daisy.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Xml.Linq;

namespace Daisy.Common
{
    public static class Utilities
    {
        public static void EncryptData()
        {
            var currentDir = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var configFile = Path.Combine(currentDir, "web.config");
            XDocument xDoc = XDocument.Load(configFile);
            bool hasChanged = false;

            var connection = xDoc.Descendants("connectionStrings").Elements("add").FirstOrDefault();
            var connectionString = connection.Attribute("connectionString").Value;
            if(!Encryption.IsEncrypt(connectionString))
            {
                connection.Attribute("connectionString").Value = Encryption.Encrypt(connectionString);
                hasChanged = true;
            }

            XElement smtpNetwork = xDoc.Descendants("network").FirstOrDefault();
            string password = smtpNetwork.Attribute("password").Value;
            if (!Encryption.IsEncrypt(password))
            {
                smtpNetwork.Attribute("password").Value = Encryption.Encrypt(password);
                hasChanged = true;
            }

            if(hasChanged)
            {
                xDoc.Save(configFile);
            }
        }

        //private static void Encrypt()
        //{
        //    var configuration = WebConfigurationManager.OpenWebConfiguration("~");

        //    var connectionSection = configuration.ConnectionStrings;
        //    foreach (ConnectionStringSettings item in connectionSection.ConnectionStrings)
        //    {
        //        if (!Encryption.IsEncrypt(item.ConnectionString))
        //        {
        //            var connectionString = Encryption.Encrypt(item.ConnectionString);
        //            item.ConnectionString = connectionString;
        //        }
        //    }

        //    SmtpSection smtpSection = (SmtpSection)configuration.GetSection("system.net/mailSettings/smtp");
        //    var password = smtpSection.Network.Password;
        //    if (!Encryption.IsEncrypt(password))
        //    {
        //        smtpSection.Network.Password = Encryption.Encrypt(password);
        //    }

        //    configuration.Save(ConfigurationSaveMode.Modified);
        //}

    }
}
