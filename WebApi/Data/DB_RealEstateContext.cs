using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Models;

#nullable disable

namespace WebApi.Data
{
    public partial class DB_RealEstateContext : DbContext
    {
        public DB_RealEstateContext()
        {
        }

        public DB_RealEstateContext(DbContextOptions<DB_RealEstateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.ToTable("Owner");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).HasColumnType("image");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.IdProperty);

                entity.ToTable("Property");

                entity.Property(e => e.IdProperty).HasColumnName("idProperty");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CodeInternal)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_Owner");
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(e => new { e.IdPropertyImage, e.IdProperty });

                entity.ToTable("PropertyImage");

                entity.Property(e => e.IdPropertyImage).ValueGeneratedOnAdd();

                entity.Property(e => e.IdProperty).HasColumnName("idProperty");

                entity.Property(e => e.File)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("file");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImages)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyImage_Property");
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace);

                entity.ToTable("PropertyTrace");

                entity.Property(e => e.IdPropertyTrace)
                    .ValueGeneratedNever()
                    .HasColumnName("idPropertyTrace");

                entity.Property(e => e.DateSale).HasColumnType("datetime");

                entity.Property(e => e.IdProperty).HasColumnName("idProperty");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Tax)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyTraces)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("FK_PropertyTrace_Property");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
