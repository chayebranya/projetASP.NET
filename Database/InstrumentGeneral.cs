//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VegeMoteur.App.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class InstrumentGeneral
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstrumentGeneral()
        {
            this.InstrumentSpecifiques = new HashSet<InstrumentSpecifique>();
        }
    
        public int Id { get; set; }
        public int CodeInterne { get; set; }
        public string Designation { get; set; }
        public bool AEtendu { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstrumentSpecifique> InstrumentSpecifiques { get; set; }
    }
}
