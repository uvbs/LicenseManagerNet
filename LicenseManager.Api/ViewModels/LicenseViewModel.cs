using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManager.Api.ViewModels
{
    public class LicenseViewModel
    {
        public int Id { get; set; }
        public string Edition { get; set; }
        public string ActivationKey { get; set; }
        public bool VolumeLicense { get; set; }
        //public string UserId { get; set; }
    }
}