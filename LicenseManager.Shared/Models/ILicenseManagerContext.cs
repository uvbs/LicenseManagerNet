using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.Models
{
    public interface ILicenseManagerContext : IDisposable
    {
        DbSet<Manufacturer> Manufacturers { get; }
        int SaveChanges();
        void MarkAsModified(Manufacturer item);
    }
}
