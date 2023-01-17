namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class gamenights
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gamenights()
        {
            foodstuffs = new HashSet<foodstuffs>();
            gamenightFood = new HashSet<gamenightFood>();
            usergamenight = new HashSet<usergamenight>();
        }

        public int Id { get; set; }

        public int OrganizingUser { get; set; }

        public int Adress { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        public int Game { get; set; }

        public bool isPotluck { get; set; }

        public virtual adresses adresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<foodstuffs> foodstuffs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<gamenightFood> gamenightFood { get; set; }

        public virtual games games { get; set; }

        public virtual users users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usergamenight> usergamenight { get; set; }
    }
}
