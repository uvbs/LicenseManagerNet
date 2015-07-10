using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LicenseManager.Api.Controllers;
using System.Web.Http.Results;
using LicenseManager.Shared.Models;
using System.Net;

namespace LicenseManager.Api.Tests.Controllers
{
    [TestClass]
    public class ManufacturersControllerTest
    {
        [TestMethod]
        public void GetManufacturers_ShouldReturnAllManufacturers()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(new Manufacturer { Id = 1, Name = "Manufacturer 1" });
            context.Manufacturers.Add(new Manufacturer { Id = 2, Name = "Manufacturer 2" });
            context.Manufacturers.Add(new Manufacturer { Id = 3, Name = "Manufacturer 3" });
            var controller = new ManufacturersController(context);

            var result = controller.GetManufacturers() as TestManufacturerDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetManufacturer_ShouldReturnManufacturerWithSameId()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(GetDemoManufacturer());
            var controller = new ManufacturersController(context);

            var result = controller.GetManufacturer(3) as OkNegotiatedContentResult<Manufacturer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);
        }

        [TestMethod]
        public void PutManufacturer_ShouldReturnStatusCode()
        {
            var controller = new ManufacturersController(new TestLicenseManagerContext());
            var item = GetDemoManufacturer();

            var result = controller.PutManufacturer(item.Id, item) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutManufacturer_ShouldFail_WhenDifferentId()
        {
            var controller = new ManufacturersController(new TestLicenseManagerContext());

            var badresult = controller.PutManufacturer(999, GetDemoManufacturer());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostManufacturer_ShouldReturnSameManufacturer()
        {
            var controller = new ManufacturersController(new TestLicenseManagerContext());
            var item = GetDemoManufacturer();

            var result = controller.PostManufacturer(item) as CreatedAtRouteNegotiatedContentResult<Manufacturer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        [TestMethod]
        public void DeleteManufacturer_ShouldReturnOk()
        {
            var context = new TestLicenseManagerContext();
            var item = GetDemoManufacturer();
            context.Manufacturers.Add(item);
            var controller = new ManufacturersController(context);

            var result = controller.DeleteManufacturer(item.Id) as OkNegotiatedContentResult<Manufacturer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);

        }

        private Manufacturer GetDemoManufacturer()
        {
            return new Manufacturer { Id = 3, Name = "Demo Manufacturer" };
        }
    }
}
