using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace LicenseManager.Shared.Models
{
    public class Software
    {
        public int Id { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
