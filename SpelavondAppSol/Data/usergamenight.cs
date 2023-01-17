namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usergamenight")]
    public partial class usergamenight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Userid { get; set; }

        public int GameNightid { get; set; }

        public virtual gamenights gamenights { get; set; }

        public virtual users users { get; set; }
    }
}
