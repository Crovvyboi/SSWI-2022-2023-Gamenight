using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<Foodstuffs>().HasOne<User>(x => x.BroughtBy).WithMany().HasForeignKey(x => x.userid).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameNight>().HasOne<Game>(x => x.PlayedGame).WithMany().HasForeignKey(x => x.GameID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<GameNight>().HasOne<User>(x => x.Organizer).WithMany().HasForeignKey(x => x.OrganizerID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasMany(x => x.playerat)
                .WithMany(x => x.Players)
                .UsingEntity<PlayerGamenight>(
                    j => j.HasOne(t => t.GameNight).WithMany(pt => pt.PlayersAdditions).HasForeignKey(t => t.gameNightID).OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne(t => t.Player).WithMany(pt => pt.playergames).HasForeignKey(t => t.playerID).OnDelete(DeleteBehavior.NoAction)
                );
            
            modelBuilder.Entity<GameNight>().HasMany<Foodstuffs>(x => x.Food).WithOne().OnDelete(DeleteBehavior.Cascade) ;

        }


        public DbSet<Game> Game { get; set; }
        public DbSet<Foodstuffs> Foodstuffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameNight> GameNight { get; set; }
        public DbSet<PlayerGamenight> userGamenights { get; set; }


    }

}
