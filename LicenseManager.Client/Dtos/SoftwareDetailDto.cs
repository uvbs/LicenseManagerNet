using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.Dtos
{
    public class SoftwareDetailDto : SoftwareDto
    {
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public int GenreId { get; set; }
        public List<LicenseDto> Licenses { get; set; }
    }
}
