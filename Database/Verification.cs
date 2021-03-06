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
    
    public partial class Verification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Verification()
        {
            this.ErreurContactPartiels = new HashSet<ErreurContactPartiel>();
            this.ErreurContactPleineTouches = new HashSet<ErreurContactPleineTouche>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public int InstrumentSpecifiqueId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ErreurContactPartiel> ErreurContactPartiels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ErreurContactPleineTouche> ErreurContactPleineTouches { get; set; }
        public virtual InstrumentSpecifique InstrumentSpecifique { get; set; }
    }
}
