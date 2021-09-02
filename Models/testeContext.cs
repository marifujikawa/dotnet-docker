using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

#nullable disable

namespace teste.Models
{
    public partial class testeContext : DbContext
    {
        public testeContext()
        {
        }

        public testeContext(DbContextOptions<testeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<Power> Powers { get; set; }
        public virtual DbSet<HeroPowers> HeroPowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=ConnectionStrings:Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<HeroPowers>(e =>
            {
                e.HasKey(i => new { i.HeroId, i.PowerId });
                e.ToTable("HeroPower");
            });
            builder.Entity<Hero>()
                 .HasMany(s => s.Powers)
                 .WithMany(c => c.Heroes)
                 .UsingEntity<HeroPowers>(
                    pt => pt
                    .HasOne(p => p.Hero)
                    .WithMany()
                    .HasForeignKey("HeroId"),
                pt => pt
                    .HasOne(p => p.Power)
                    .WithMany()
                    .HasForeignKey("PowerId"))
                .ToTable("HeroPower")
                .HasKey(pt => new { pt.HeroId, pt.PowerId });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
