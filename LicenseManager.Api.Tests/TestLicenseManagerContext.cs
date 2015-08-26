using LicenseManager.Shared;
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
            this.Licenses = new TestLicenseDbSet();
            this.Genres = new TestGenreDbSet();
            this.Computers = new TestComputerDbSet();
            this.UsedOns = new TestUsedOnDbSet();
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<UsedOn> UsedOns { get; set; }

        public void Dispose()
        {
            
        }

        public void MarkAsModified(object item)
        {
            
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
