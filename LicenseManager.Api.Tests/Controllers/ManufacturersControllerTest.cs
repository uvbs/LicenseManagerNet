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
