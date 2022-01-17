using System;
using LabReservation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LabReservation.Data.DataContext

{
    public partial class LabReservationContext : DbContext
    {
        public LabReservationContext()
        {
        }

        public LabReservationContext(DbContextOptions<LabReservationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Lab> Labs { get; set; }
        public virtual DbSet<LabTest> LabTests { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=WX-5CG7476PXB;Initial Catalog=LabReservation;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasIndex(e => new { e.CityId, e.Name, e.LabId }, "IX_Areas_CityId_Name")
                    .IsUnique();

                entity.Property(e => e.IsAtHome)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.CityId);

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Areas_Lab");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasIndex(e => e.AreaId, "IX_Branchs_AreaId");

                entity.HasIndex(e => e.LabId, "IX_Branchs_LabId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.AreaId);

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.LabId);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Cities_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.Property(e => e.HomeFees).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<LabTest>(entity =>
            {
                entity.HasIndex(e => e.TestId, "IX_LabTests_TestId");

                entity.Property(e => e.Fees).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.LabTests)
                    .HasForeignKey(d => d.LabId);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.LabTests)
                    .HasForeignKey(d => d.TestId);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.ReserveTime).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Areas");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Labs");
            });

            modelBuilder.Entity<ReservationDetail>(entity =>
            {
                entity.HasOne(d => d.LabTest)
                    .WithMany(p => p.ReservationDetails)
                    .HasForeignKey(d => d.LabTestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationDetails_LabTests");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationDetails)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationDetails_Reservation");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
