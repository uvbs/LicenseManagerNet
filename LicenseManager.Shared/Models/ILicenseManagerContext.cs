using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseManager.Shared;

namespace LicenseManager.Shared.Models
{
    public interface ILicenseManagerContext : IDisposable
    {
        DbSet<Manufacturer> Manufacturers { get; }
        DbSet<Software> Softwares { get; }
        DbSet<License> Licenses { get; }
        DbSet<Genre> Genres { get; }
        DbSet<Computer> Computers { get; }
        DbSet<UsedOn> UsedOns { get; }

        int SaveChanges();
        void MarkAsModified(object item);
    }
}
