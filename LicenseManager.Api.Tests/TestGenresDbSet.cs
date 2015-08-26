using LicenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Api.Tests
{
    public class TestGenreDbSet : TestDbSet<Genre>
    {
        public override Genre Find(params object[] keyValues)
        {
            return this.SingleOrDefault(g => g.GenreId == (int)keyValues.Single());
        }
    }
}
