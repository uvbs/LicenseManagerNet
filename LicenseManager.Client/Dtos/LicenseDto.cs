using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.Dtos
{
    public class LicenseDto
    {
        public int Id { get; set; }
        public string Edition { get; set; }
        public string ActivationKey { get; set; }
        public bool VolumeLicense { get; set; }
    }
}
