using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LicenseManager.Shared.Models;
using LicenseManager.Api.Controllers;
using System.Web.Http.Results;
using System.Net;

namespace LicenseManager.Api.Tests.Controllers
{
    [TestClass]
    public class LicensesControllerTest
    {
        [TestMethod]
        public void GetLicenses_ShouldReturnAllLicenses()
        {
            var context = GetDemoContext();
            var controller = new LicensesController(context);

            var result = controller.GetLicenses() as TestLicenseDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Licenses.Local.Count, result.Local.Count);
        }

        [TestMethod]
        public void GetLicense_ShouldReturnCorrectLicense()
        {
            var context = GetDemoContext();
            var item = GetDemoItem();
            context.Licenses.Add(item);
            var controller = new LicensesController(context);

            var result = controller.GetLicense(item.Id) as OkNegotiatedContentResult<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
            Assert.AreEqual(item.Key, result.Content.Key);
        }

        [TestMethod]
        public void PutLicense_ShouldReturnStatusCode()
        {
            var controller = new LicensesController(new TestLicenseManagerContext());
            var item = GetDemoItem();

            var result = controller.PutLicense(item.Id, item) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PostLicense_ShouldReturnSameLicense()
        {
            var controller = new LicensesController(GetDemoContext());
            var item = GetDemoItem();

            var result = controller.PostLicense(item) as CreatedAtRouteNegotiatedContentResult<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Key, item.Key);
        }

        [TestMethod]
        public void DeleteLicense_ShouldReturnOk()
        {
            var context = GetDemoContext();
            var item = GetDemoItem();
            context.Licenses.Add(item);
            var controller = new LicensesController(context);

            var result = controller.DeleteLicense(item.Id) as OkNegotiatedContentResult<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Id, item.Id);
        }

        private TestLicenseManagerContext GetDemoContext()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(new Manufacturer { Id = 1, Name = "Demo Manufacturer 1" });
            context.Softwares.Add(new Software { Id = 1, Name = "Demo Software 1", ManufacturerId = 1, Genre = Shared.Enums.Genre.Others });
            context.Softwares.Add(new Software { Id = 2, Name = "Demo Software 2", ManufacturerId = 1, Genre = Shared.Enums.Genre.Others });

            context.Licenses.Add(new License { Id = 1, SoftwareId = 1, Key = "DEMO-KEY1" });
            context.Licenses.Add(new License { Id = 2, SoftwareId = 1, Key = "DEMO-KEY2" });
            context.Licenses.Add(new License { Id = 3, SoftwareId = 2, Key = "DEMO-KEY3" });

            return context;
        }

        private License GetDemoItem()
        {
            return new License { Id = 4, SoftwareId = 1, Key = "DEMO-KEY4" };
        }
    }
}
