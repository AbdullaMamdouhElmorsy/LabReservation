using System;
using WebApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Data.DataContext

{
    public partial class WebApplicationContext : DbContext
    {
        public WebApplicationContext()
        {
        }

        public WebApplicationContext(DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=WX-5CG7476PXB;Initial Catalog=WebApplication;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasIndex(e => new { e.CityId}, "IX_Areas_CityId_Name")
                    .IsUnique();

          
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.CityId);

            
            });

          

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Cities_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

          
         
         


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
