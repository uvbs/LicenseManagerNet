using System.Web.UI;
using LicenseManager.Api.Controllers;
using LicenseManager.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Http.Results;

namespace LicenseManager.Api.Tests.Controllers
{
    [TestClass]
    public class SoftwaresControllerTest
    {
        [TestMethod]
        public void GetSoftwares_ShouldReturnAllSoftwares()
        {
            var context = GetDemoContext();
            var controller = new SoftwaresController(context);

            var result = controller.GetSoftwares() as TestSoftwareDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Softwares.Local.Count, result.Local.Count);
        }

        [TestMethod]
        public void GetSoftware_ShouldReturnSoftwareWithSameId()
        {
            var context = GetDemoContext();
            var item = GetDemoSoftware();
            context.Softwares.Add(item);
            var controller = new SoftwaresController(context);

            var result = controller.GetSoftware(-1, item.SoftwareId) as OkNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.SoftwareId, item.SoftwareId);
        }

        [TestMethod]
        public void GetSoftware_ShouldReturnNotFound_WhenInvalidId()
        {
            var controller = new SoftwaresController(new TestLicenseManagerContext());

            var result = controller.GetSoftware(-1, 999);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutSoftware_ShouldReturnStatusCode()
        {
            var controller = new SoftwaresController(GetDemoContext());
            var item = GetDemoSoftware();

            var result = controller.PutSoftware(item.SoftwareId, item) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutSoftware_ShouldFail_WhenDifferentId()
        {
            var controller = new SoftwaresController(new TestLicenseManagerContext());

            var badresult = controller.PutSoftware(999, GetDemoSoftware());

            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PutSoftware_ShouldFail_WhenInvalidManufacturerId()
        {
            var context = GetDemoContext();
            var item = GetDemoSoftware();
            context.Softwares.Add(item);
            var controller = new SoftwaresController(context);
            item.ManufacturerId = 999;

            var result = controller.PutSoftware(item.SoftwareId, item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void PostSoftware_ShouldReturnSameSoftware()
        {
            var controller = new SoftwaresController(GetDemoContext());
            var item = GetDemoSoftware();

            var result = controller.PostSoftware(item) as CreatedAtRouteNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.SoftwareId);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        [TestMethod]
        public void PostSoftware_ShouldFail_WhenInvalidManufacturerId()
        {
            var controller = new SoftwaresController(new TestLicenseManagerContext());
            var item = GetDemoSoftware();

            var result = controller.PostSoftware(item);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void DeleteSoftware_ShouldReturnOk()
        {
            var context = GetDemoContext();
            var item = GetDemoSoftware();
            context.Softwares.Add(item);
            var controller = new SoftwaresController(context);

            var result = controller.DeleteSoftware(item.SoftwareId) as OkNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.SoftwareId, result.Content.SoftwareId);
        }

        private Software GetDemoSoftware()
        {
            return new Software { SoftwareId = 3, Description = "Demo", Name = "Demo Software 3", ManufacturerId = 1, GenreId = 1 };
        }

        private TestLicenseManagerContext GetDemoContext()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(new Manufacturer { ManufacturerId = 1, Name = "DemoManufacturer" });
            context.Genres.Add(new Genre { GenreId = 1, Name = "Demo Genre 1" });
            context.Softwares.Add(new Software { SoftwareId = 1, Name = "Demo Software 1", ManufacturerId = 1, Description = "Demo1", GenreId = 1 });
            context.Softwares.Add(new Software { SoftwareId = 2, Name = "Demo Software 2", ManufacturerId = 1, Description = "Demo2", GenreId = 1 });

            return context;
        }
    }
}
