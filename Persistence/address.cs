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
    
    public partial class address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public address()
        {
            this.locations = new HashSet<location>();
        }
    
        public long address_id { get; set; }
        public long town_fk { get; set; }
        public string streetname { get; set; }
        public string streetnumber { get; set; }
        public string country { get; set; }
        public string additive { get; set; }
        public Nullable<long> po_Box { get; set; }
    
        public virtual town town { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<location> locations { get; set; }
    }
}
