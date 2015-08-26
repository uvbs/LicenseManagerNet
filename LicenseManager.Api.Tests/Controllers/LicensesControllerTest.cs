using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LicenseManager.Shared;
using LicenseManager.Shared.Models;
using LicenseManager.Api.Controllers;
using System.Web.Http.Results;
using System.Net;
using System.Linq;

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

            var result = controller.GetLicense(item.LicenseId) as OkNegotiatedContentResult<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.LicenseId, result.Content.LicenseId);
            Assert.AreEqual(item.ActivationKey, result.Content.ActivationKey);
        }

        [TestMethod]
        public void GetLicensesBySoftwareId_ShouldReturnLicensesOfThisSoftware()
        {
            var controller = new LicensesController(GetDemoContext());

            var result = controller.GetLicensesBySoftwareId(1) as IQueryable<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.FirstOrDefault().SoftwareId, 1);
        }

        [TestMethod]
        public void PutLicense_ShouldReturnStatusCode()
        {
            var controller = new LicensesController(new TestLicenseManagerContext());
            var item = GetDemoItem();

            var result = controller.PutLicense(item.LicenseId, item) as StatusCodeResult;

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
            Assert.AreEqual(result.RouteValues["id"], result.Content.LicenseId);
            Assert.AreEqual(result.Content.ActivationKey, item.ActivationKey);
        }

        [TestMethod]
        public void DeleteLicense_ShouldReturnOk()
        {
            var context = GetDemoContext();
            var item = GetDemoItem();
            context.Licenses.Add(item);
            var controller = new LicensesController(context);

            var result = controller.DeleteLicense(item.LicenseId) as OkNegotiatedContentResult<License>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.LicenseId, item.LicenseId);
        }

        private TestLicenseManagerContext GetDemoContext()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(new Manufacturer { ManufacturerId = 1, Name = "Demo Manufacturer 1" });
            context.Genres.Add(new Genre { GenreId = 1, Name = "Demo Genre 1" });
            context.Softwares.Add(new Software { SoftwareId = 1, Name = "Demo Software 1", ManufacturerId = 1, GenreId = 1 });
            context.Softwares.Add(new Software { SoftwareId = 2, Name = "Demo Software 2", ManufacturerId = 1, GenreId = 1 });

            context.Licenses.Add(new License { LicenseId = 1, SoftwareId = 1, ActivationKey = "DEMO-KEY1" });
            context.Licenses.Add(new License { LicenseId = 2, SoftwareId = 1, ActivationKey = "DEMO-KEY2" });
            context.Licenses.Add(new License { LicenseId = 3, SoftwareId = 2, ActivationKey = "DEMO-KEY3" });

            return context;
        }

        private License GetDemoItem()
        {
            return new License { LicenseId = 4, SoftwareId = 1, ActivationKey = "DEMO-KEY4" };
        }
    }
}
