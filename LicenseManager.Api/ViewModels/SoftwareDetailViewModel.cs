using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManager.Api.ViewModels
{
    public class SoftwareDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }

        public IEnumerable<LicenseViewModel> Licenses { get; set; }
    }
}