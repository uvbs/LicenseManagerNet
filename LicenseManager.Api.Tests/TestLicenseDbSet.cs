using LicenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Api.Tests
{
    public class TestLicenseDbSet : TestDbSet<License>
    {
        public override License Find(params object[] keyValues)
        {
            return this.SingleOrDefault(l => l.Id == (int)keyValues.Single());
        }
    }
}
