using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LicenseManager.Shared;
using LicenseManager.Api.Controllers;

namespace LicenseManager.Api.Tests.Controllers
{
    [TestClass]
    public class ComputersControllerTest
    {
        [TestMethod]
        public void GetComputers_ShouldReturnAllComputers()
        {
            var context = GetDemoContext();
            var controller = new ComputersController(context);

            var result = controller.GetComputers() as TestComputerDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Computers.Local.Count, result.Local.Count);
        }

        [TestMethod]
        public void GetComputersByUser_ShouldOnlyReturnComputersOfTheUser()
        {
            var context = GetDemoContext();
            var controller = new ComputersController(context);

            var result = controller.GetComputers("686990B4-3263-4566-9713-27666E894AF3") as TestComputerDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Local.Count);
        }

        

        private TestLicenseManagerContext GetDemoContext()
        {
            var context = new TestLicenseManagerContext();
            context.Computers.Add(new Computer
            {
                ComputerId = 1,
                UserId = "686990B4-3263-4566-9713-27666E894AF3",
                Hostname = "Demo Computer 1",
                IpAddress = "127.0.0.1",
                Location = "Demo Location"
            });
            context.Computers.Add(new Computer
            {
                ComputerId = 2,
                UserId = "92F7EBC3-9AC1-492F-ADC5-2CFC4348829E",
                Hostname = "Demo Computer 2",
                IpAddress = "127.0.0.1",
                Location = "Demo Location"
            });
            context.Computers.Add(new Computer
            {
                ComputerId = 3,
                UserId = "686990B4-3263-4566-9713-27666E894AF3",
                Hostname = "Demo Computer 3",
                IpAddress = "127.0.0.1",
                Location = "Demo location"
            });
            return context;
        }

        private Computer GetDemoComputer()
        {
            return new Computer
            {
                ComputerId = 4,
                UserId = "92F7EBC3-9AC1-492F-ADC5-2CFC4348829E",
                Hostname = "Demo Computer 4",
                IpAddress = "127.0.0.1",
                Location = "other location"
            };
        }
    }
}
