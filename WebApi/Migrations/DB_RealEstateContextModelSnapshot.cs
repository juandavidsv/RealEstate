// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Data;

namespace WebApi.Migrations
{
    [DbContext(typeof(DB_RealEstateContext))]
    partial class DB_RealEstateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Models.Owner", b =>
                {
                    b.Property<int>("IdOwner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("image");

                    b.HasKey("IdOwner");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("WebApi.Models.Property", b =>
                {
                    b.Property<int>("IdProperty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idProperty")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("IdOwner")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<short?>("Year")
                        .HasColumnType("smallint");

                    b.HasKey("IdProperty");

                    b.HasIndex("IdOwner");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("WebApi.Models.PropertyImage", b =>
                {
                    b.Property<int>("IdPropertyImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdProperty")
                        .HasColumnType("int")
                        .HasColumnName("idProperty");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("image")
                        .HasColumnName("file");

                    b.HasKey("IdPropertyImage", "IdProperty");

                    b.HasIndex("IdProperty");

                    b.ToTable("PropertyImage");
                });

            modelBuilder.Entity("WebApi.Models.PropertyTrace", b =>
                {
                    b.Property<int>("IdPropertyTrace")
                        .HasColumnType("int")
                        .HasColumnName("idPropertyTrace");

                    b.Property<DateTime?>("DateSale")
                        .HasColumnType("datetime");

                    b.Property<int?>("IdProperty")
                        .HasColumnType("int")
                        .HasColumnName("idProperty");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Tax")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.Property<string>("Value")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true);

                    b.HasKey("IdPropertyTrace");

                    b.HasIndex("IdProperty");

                    b.ToTable("PropertyTrace");
                });

            modelBuilder.Entity("WebApi.Models.Property", b =>
                {
                    b.HasOne("WebApi.Models.Owner", "IdOwnerNavigation")
                        .WithMany("Properties")
                        .HasForeignKey("IdOwner")
                        .HasConstraintName("FK_Property_Owner")
                        .IsRequired();

                    b.Navigation("IdOwnerNavigation");
                });

            modelBuilder.Entity("WebApi.Models.PropertyImage", b =>
                {
                    b.HasOne("WebApi.Models.Property", "IdPropertyNavigation")
                        .WithMany("PropertyImages")
                        .HasForeignKey("IdProperty")
                        .HasConstraintName("FK_PropertyImage_Property")
                        .IsRequired();

                    b.Navigation("IdPropertyNavigation");
                });

            modelBuilder.Entity("WebApi.Models.PropertyTrace", b =>
                {
                    b.HasOne("WebApi.Models.Property", "IdPropertyNavigation")
                        .WithMany("PropertyTraces")
                        .HasForeignKey("IdProperty")
                        .HasConstraintName("FK_PropertyTrace_Property");

                    b.Navigation("IdPropertyNavigation");
                });

            modelBuilder.Entity("WebApi.Models.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("WebApi.Models.Property", b =>
                {
                    b.Navigation("PropertyImages");

                    b.Navigation("PropertyTraces");
                });
#pragma warning restore 612, 618
        }
    }
}
