//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class devicetype
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public devicetype()
        {
            this.devices = new HashSet<device>();
            this.operatingsystems = new HashSet<operatingsystem>();
        }
    
        public long deviceType_id { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<device> devices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<operatingsystem> operatingsystems { get; set; }
    }
}
