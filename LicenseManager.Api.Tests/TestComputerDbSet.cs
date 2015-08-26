using LicenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Api.Tests
{
    public class TestComputerDbSet : TestDbSet<Computer>
    {
        
        public override Computer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.ComputerId == (int)keyValues.Single());
        }
    }
}
