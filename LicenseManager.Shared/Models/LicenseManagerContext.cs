﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LicenseManager.Shared.Models
{
    public class LicenseManagerContext : DbContext, ILicenseManagerContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public LicenseManagerContext()
            : base("name=LicenseManagerEntities")
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<UsedOn> UsedOns { get; set; }

        public void MarkAsModified(object item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
