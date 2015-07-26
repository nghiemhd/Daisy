using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlickrAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = "0fcae91b162fddb2";
            var key = "d15bf67c8d6b7aedb85c4a9c43fd7bc0";

            var methodGetFrob = "flickr.auth.getFrob";

            var sig = secret + "api_key" + key + "method" + methodGetFrob;

            string signature = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                signature = GetMd5Hash(md5Hash, sig);
            }

            string request = "https://api.flickr.com/services/rest/?method=" + methodGetFrob + "&api_key=" + key + "&api_sig=" + signature;
            Console.WriteLine("GET frob request: " + request);

            string frob = GetFrob(request);

            sig = secret + "api_key" + key + "frob" + frob + "permswrite";

            using (MD5 md5Hash = MD5.Create())
            {
                signature = GetMd5Hash(md5Hash, sig);
            }

            request = "http://www.flickr.com/services/auth/?api_key=" + key + "&perms=write&frob=" + frob + "&api_sig=" + signature;

            var methodGetToken = "flickr.auth.getToken";
            sig = secret + "api_key" + key + "frob" + frob + "method" + methodGetToken;
            using (MD5 md5Hash = MD5.Create())
            {
                signature = GetMd5Hash(md5Hash, sig);
            }
            request = "https://api.flickr.com/services/rest/?method=" + methodGetToken + "&api_key=" + key + "&frob=" + frob + "&api_sig=" + signature;

            string token = GetToken(request);
        }

        private static string GetToken(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webResponse.Close();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var token = xmlDocument.GetElementsByTagName("token")[0].InnerText;

            return token;
        }

        private static string GetFrob(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webResponse.Close();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var frob = xmlDocument.GetElementsByTagName("frob")[0].InnerText;

            return frob;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}
