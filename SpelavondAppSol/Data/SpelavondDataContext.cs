using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Data
{
    public partial class SpelavondDataContext : DbContext
    {
        public SpelavondDataContext()
            : base("name=SpelavondDataContext")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<adresses> adresses { get; set; }
        public virtual DbSet<foodstuffs> foodstuffs { get; set; }
        public virtual DbSet<gamenightFood> gamenightFood { get; set; }
        public virtual DbSet<gamenights> gamenights { get; set; }
        public virtual DbSet<games> games { get; set; }
        public virtual DbSet<usergamenight> usergamenight { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<adresses>()
                .HasMany(e => e.gamenights)
                .WithRequired(e => e.adresses)
                .HasForeignKey(e => e.Adress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<adresses>()
                .HasMany(e => e.users)
                .WithRequired(e => e.adresses)
                .HasForeignKey(e => e.Adress);

            modelBuilder.Entity<foodstuffs>()
                .HasMany(e => e.gamenightFood)
                .WithRequired(e => e.foodstuffs)
                .HasForeignKey(e => e.Foodid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gamenights>()
                .HasMany(e => e.foodstuffs)
                .WithOptional(e => e.gamenights)
                .HasForeignKey(e => e.Foodstuffs1);

            modelBuilder.Entity<gamenights>()
                .HasMany(e => e.gamenightFood)
                .WithRequired(e => e.gamenights)
                .HasForeignKey(e => e.GameNightid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gamenights>()
                .HasMany(e => e.usergamenight)
                .WithRequired(e => e.gamenights)
                .HasForeignKey(e => e.GameNightid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<games>()
                .HasMany(e => e.gamenights)
                .WithRequired(e => e.games)
                .HasForeignKey(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.gamenights)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.OrganizingUser);

            modelBuilder.Entity<users>()
                .HasMany(e => e.usergamenight)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.Userid)
                .WillCascadeOnDelete(false);
        }
    }
}
