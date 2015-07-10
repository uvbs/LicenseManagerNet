using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LicenseManager.Api.Controllers;
using System.Web.Http.Results;
using LicenseManager.Shared.Models;

namespace LicenseManager.Api.Tests.Controllers
{
    [TestClass]
    public class ManufacturersControllerTest
    {
        [TestMethod]
        public void GetProducts_ShouldReturnAllProducts()
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

        private Manufacturer GetDemoManufacturer()
        {
            return new Manufacturer { Id = 3, Name = "Demo Manufacturer" };
        }
    }
}
