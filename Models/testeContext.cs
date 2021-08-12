using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<HeroPower> HeroPowers { get; set; }
        public virtual DbSet<Power> Powers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Hero>(entity =>
            {
                entity.ToTable("hero");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<HeroPower>(entity =>
            {
                entity.ToTable("hero_power");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.HeroId).HasColumnName("hero_id");

                entity.Property(e => e.PowerId).HasColumnName("power_id");

                entity.HasOne(d => d.Hero)
                    .WithMany(p => p.HeroPowers)
                    .HasForeignKey(d => d.HeroId)
                    .HasConstraintName("hero_power_fk");

                entity.HasOne(d => d.Power)
                    .WithMany(p => p.HeroPowers)
                    .HasForeignKey(d => d.PowerId)
                    .HasConstraintName("hero_power_fk_1");
            });

            modelBuilder.Entity<Power>(entity =>
            {
                entity.ToTable("power");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
