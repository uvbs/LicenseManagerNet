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
    }
}