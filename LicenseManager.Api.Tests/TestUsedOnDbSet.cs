using LicenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Api.Tests
{
    public class TestUsedOnDbSet : TestDbSet<UsedOn>
    {
        public override UsedOn Find(params object[] keyValues)
        {
            return this.SingleOrDefault(u => u.UsedOnId.Equals((string)keyValues.Single()));
        }
    }
}
