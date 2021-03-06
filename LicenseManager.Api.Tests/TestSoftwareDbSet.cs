﻿using LicenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseManager.Api.Tests
{
    public class TestSoftwareDbSet : TestDbSet<Software>
    {
        public override Software Find(params object[] keyValues)
        {
            return this.SingleOrDefault(s => s.SoftwareId == (int)keyValues.Single());
        }
    }
}
