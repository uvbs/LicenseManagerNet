﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.Dtos
{
    public class ManufacturerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Homepage { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
