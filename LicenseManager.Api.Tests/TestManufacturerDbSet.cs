using LicenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Api.Tests
{
    public class TestManufacturerDbSet : TestDbSet<Manufacturer>
    {
        public override Manufacturer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(m => m.ManufacturerId == (int)keyValues.Single());
        }
    }
}
