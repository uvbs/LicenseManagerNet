using LicenseManager.Api.Controllers;
using LicenseManager.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

        //[TestMethod]
        public void GetSoftware_ShouldReturnSoftwareWithSameId()
        {
            var context = GetDemoContext();
            var item = GetDemoSoftware();
            context.Softwares.Add(item);
            var controller = new SoftwaresController(context);

            var result = controller.GetSoftware(item.Id) as OkNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Id, item.Id);
        }

        //[TestMethod]
        public void PutSoftware_ShouldReturnStatusCode()
        {
            var controller = new SoftwaresController(new TestLicenseManagerContext());
            var item = GetDemoSoftware();

            var result = controller.PutSoftware(item.Id, item) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        //[TestMethod]
        public void PutSoftware_ShouldFail_WhenDifferentId()
        {
            var controller = new SoftwaresController(new TestLicenseManagerContext());

            var badresult = controller.PutSoftware(999, GetDemoSoftware());

            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        //[TestMethod]
        public void PostSoftware_ShouldReturnSameSoftware()
        {
            var controller = new SoftwaresController(GetDemoContext());
            var item = GetDemoSoftware();

            var result = controller.PostSoftware(item) as CreatedAtRouteNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        //[TestMethod]
        public void DeleteSoftware_ShouldReturnOk()
        {
            var context = GetDemoContext();
            var item = GetDemoSoftware();
            context.Softwares.Add(item);
            var controller = new SoftwaresController(context);

            var result = controller.DeleteSoftware(item.Id) as OkNegotiatedContentResult<Software>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        private Software GetDemoSoftware()
        {
            return new Software { Id = 3, Description = "Demo", Name = "Demo Software 3", ManufacturerId = 1 };
        }

        private TestLicenseManagerContext GetDemoContext()
        {
            var context = new TestLicenseManagerContext();
            context.Manufacturers.Add(new Manufacturer { Id = 1, Name = "DemoManufacturer" });
            context.Softwares.Add(new Software { Id = 1, Name = "Demo Software 1", ManufacturerId = 1, Description = "Demo1" });
            context.Softwares.Add(new Software { Id = 2, Name = "Demo Software 2", ManufacturerId = 1, Description = "Demo2" });

            return context;
        }
    }
}
