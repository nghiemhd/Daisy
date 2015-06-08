using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using Daisy.UnitTest.MockObjects;
using Daisy.Service;
using System.Collections.Generic;
using Daisy.Core.Entities;

namespace Daisy.UnitTest
{
    [TestClass]
    public class PhotoServiceTest
    {
        IUnitOfWork unitOfWork;
        IPhotoService photoService;

        [TestInitialize]
        public void Setup()
        {
            unitOfWork = new MockUnitOfWork<MockDataContext>();
            photoService = new PhotoService(unitOfWork);
        }

        [TestMethod]
        public void TestGetAllPhotos()
        {
            var photos = photoService.GetAllPhotos() as IList<Photo>;

            Assert.IsNotNull(photos);
            Assert.IsTrue(photos.Count > 0);
        }

        [TestMethod]
        public void TestGetDisplayedPhotos()
        {
            var displayedPhotos = photoService.GetDisplayedPhotos() as IList<Photo>;

            Assert.IsNotNull(displayedPhotos);
            Assert.IsTrue(displayedPhotos.Count == 1);
        }

        [TestMethod]
        public void TestGetPhotoById()
        {
            var id = 1;
            var photo = photoService.GetPhotoById(id);

            Assert.IsTrue(string.Equals(photo.Name, "Photo 1"));
        }

        [TestMethod]
        public void TestDeletePhotoById()
        {
            var photos = photoService.GetAllPhotos() as IList<Photo>;           
            var id = 1;
            photoService.DeletePhotoById(id);

            var expectedPhotos = photoService.GetAllPhotos() as IList<Photo>;
            
            Assert.IsTrue(expectedPhotos.Count == photos.Count - 1);
        }
    }
}
