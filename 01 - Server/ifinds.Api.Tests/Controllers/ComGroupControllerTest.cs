using System.Web.Mvc;
using ifinds.Api;
using ifinds.Api.Controllers;
using NUnit.Framework;
using DTP.Object;
using System;

namespace ifinds.Api.Tests.Controllers
{
    [TestFixture]
    public class ComGroupControllerTest
    {
        [Test]
        public void Get()
        {
            // Arrange
            ComGroupController controller = new ComGroupController();
            // Act
             
            ComGroup result = controller.Get("COM0000001");

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [Test]
        public void Post()
        {
            // Arrange
            ComGroupController controller = new ComGroupController();
            ComGroup model = controller.Get("COM0000001");

            model.GroupDesc = "SAI Company is it comp";
            
            // Act
            ComGroup result = controller.Put("COM0000001", model) ;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
        
    }
}
