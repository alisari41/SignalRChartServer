using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SignalRChartServer.Models
{
    public partial class SatisDbContext : DbContext
    {
        public SatisDbContext()
        {
        }

        public SatisDbContext(DbContextOptions<SatisDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Personeller> Personellers { get; set; }
        public virtual DbSet<Satislar> Satislars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SatisDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Personeller>(entity =>
            {
                entity.ToTable("Personeller");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Soyadi).HasMaxLength(50);
            });

            modelBuilder.Entity<Satislar>(entity =>
            {
                entity.ToTable("Satislar");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
