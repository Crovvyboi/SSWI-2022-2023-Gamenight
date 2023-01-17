﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Foodstuffs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAlcoholic")
                        .HasColumnType("bit");

                    b.Property<bool>("isVegan")
                        .HasColumnType("bit");

                    b.Property<bool>("nutAlergy")
                        .HasColumnType("bit");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("GameNightId");

                    b.HasIndex("userid");

                    b.ToTable("Foodstuffs");
                });

            modelBuilder.Entity("Domain.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EighteenPlus")
                        .HasColumnType("bit");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PicturePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Domain.Models.GameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("HouseNumberAdditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizerID")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isEighteenPlus")
                        .HasColumnType("bit");

                    b.Property<bool>("isPotluck")
                        .HasColumnType("bit");

                    b.Property<int>("maxPlayers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameID");

                    b.HasIndex("OrganizerID");

                    b.ToTable("GameNight");
                });

            modelBuilder.Entity("Domain.Models.PlayerGamenight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("gameNightID")
                        .HasColumnType("int");

                    b.Property<int>("playerID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("gameNightID");

                    b.HasIndex("playerID");

                    b.ToTable("userGamenights");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("HouseNumberAdditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVegan")
                        .HasColumnType("bit");

                    b.Property<bool>("nutAlergy")
                        .HasColumnType("bit");

                    b.Property<bool>("toleratesAlcohol")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.Foodstuffs", b =>
                {
                    b.HasOne("Domain.Models.GameNight", null)
                        .WithMany("Food")
                        .HasForeignKey("GameNightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Models.User", "BroughtBy")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BroughtBy");
                });

            modelBuilder.Entity("Domain.Models.GameNight", b =>
                {
                    b.HasOne("Domain.Models.Game", "PlayedGame")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Organizer");

                    b.Navigation("PlayedGame");
                });

            modelBuilder.Entity("Domain.Models.PlayerGamenight", b =>
                {
                    b.HasOne("Domain.Models.GameNight", "GameNight")
                        .WithMany("PlayersAdditions")
                        .HasForeignKey("gameNightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "Player")
                        .WithMany("playergames")
                        .HasForeignKey("playerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GameNight");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Domain.Models.GameNight", b =>
                {
                    b.Navigation("Food");

                    b.Navigation("PlayersAdditions");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("playergames");
                });
#pragma warning restore 612, 618
        }
    }
}
