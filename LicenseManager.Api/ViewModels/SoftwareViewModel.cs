using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManager.Api.ViewModels
{
    public class SoftwareViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }

        public IEnumerable<LicenseViewModel> Licenses { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", ManufacturerName, Name);
        }
    }
}