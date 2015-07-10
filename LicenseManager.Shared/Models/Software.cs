using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Shared.Models
{
    public class Software
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public virtual Manufacturer Manufacturer { get; set; }
    }
}
