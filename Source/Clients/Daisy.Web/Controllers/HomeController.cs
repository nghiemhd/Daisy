using AutoMapper;
using CaptchaMvc.HtmlHelpers;
using DaisyModels = Daisy.Web.Models;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using Daisy.Common;

namespace Daisy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentService contentService;

        public HomeController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public ActionResult Index()
        {
            var photos = contentService.GetFirstSlider().Photos.ToList();
            var model = Mapper.Map<List<DaisyModels.Photo>>(photos);
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(DaisyModels.FeedbackViewModel model)
        {
            var error = "Invalid";
            if (ModelState.IsValid)
            {
                if (!this.IsCaptchaValid("The answer is not correct."))
                {
                    TempData["error"] = error;
                }
                else
                {
                    try
                    {
                        var toEmail = ConfigurationManager.AppSettings[Constants.DaisyEmail];
                        var body = "<p>Email: {0} ({1})</p>" +
                            "<p>Phone Number: {2}</p>" +
                            "<p>Message:</p><p>{3}</p>";
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(toEmail));
                        message.Subject = "[Daisy Studio] Customer's message";
                        message.Body = string.Format(body, model.Name, model.Email, model.PhoneNumber, model.Message);
                        message.IsBodyHtml = true;
                        using (var smtp = new SmtpClient())
                        {
                            await smtp.SendMailAsync(message);                            
                        }
                        TempData["message"] = "Your message has been sent successfully. We will get back to you very soon.";
                        return RedirectToAction("Contact");
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }                    
                }
            }
            else
            {
                TempData["error"] = error;
            }
            return View("Contact", model);
        }

        public FileResult Quote()
        {
            var quotePath = ConfigurationManager.AppSettings["QuotePath"];
            if (quotePath != Path.GetFullPath(quotePath))
            {
                var rootPath = Server.MapPath("~");
                quotePath = Path.Combine(rootPath, quotePath);
            }

            var directory = new DirectoryInfo(quotePath);
            var latestQuote = directory
                .GetFiles()
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();

            byte[] fileBytes = System.IO.File.ReadAllBytes(latestQuote.FullName);
            string fileName = latestQuote.Name;
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
            return File(fileBytes, "application/pdf");
        }

        public ActionResult QuotePage()
        {
            var quotePath = ConfigurationManager.AppSettings["QuotePath"]; 
            if (quotePath != Path.GetFullPath(quotePath))
            {
                var rootPath = Server.MapPath("~");
                quotePath = Path.Combine(rootPath, quotePath);
            }

            var directory = new DirectoryInfo(quotePath);
            var latestQuote = directory
                .GetFiles()
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();

            var url = MapURL(Path.Combine(quotePath, latestQuote.Name));
            var model = new DaisyModels.Quote 
            {
                Url = url
            };

            return View(model);
        }

        private string MapURL(string path)
        {
            string appPath = Server.MapPath("/").ToLower();
            return string.Format("/{0}", path.ToLower().Replace(appPath, "").Replace(@"\", "/"));
        }
    }
}