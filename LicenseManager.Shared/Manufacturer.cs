//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LicenseManager.Shared
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            this.Softwares = new HashSet<Software>();
        }
    
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Homepage { get; set; }
    
        public virtual ICollection<Software> Softwares { get; set; }
    }
}
