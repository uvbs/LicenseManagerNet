using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LicenseManager.Api.Tests
{
    public class TestLicenseManagerContext : ILicenseManagerContext
    {
        public TestLicenseManagerContext()
        {
            this.Manufacturers = new TestManufacturerDbSet();
            this.Softwares = new TestSoftwareDbSet();
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Software> Softwares { get; set; }

        public void Dispose()
        {
            
        }

        public void MarkAsModified(Manufacturer item)
        {
            
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
