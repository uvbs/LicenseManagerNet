using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Client.Dtos
{
    public class SoftwareDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string GenreName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", ManufacturerName, Name);
        }
    }
}
