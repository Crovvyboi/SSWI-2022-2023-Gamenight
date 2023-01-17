namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gamenightFood")]
    public partial class gamenightFood
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int GameNightid { get; set; }

        public int Foodid { get; set; }

        public virtual foodstuffs foodstuffs { get; set; }

        public virtual gamenights gamenights { get; set; }
    }
}
