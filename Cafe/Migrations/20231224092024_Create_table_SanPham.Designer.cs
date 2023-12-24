﻿// <auto-generated />
using Cafe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafe.Migrations
{
    [DbContext(typeof(ApplicationDbcontext))]
    [Migration("20231224092024_Create_table_SanPham")]
    partial class Create_table_SanPham
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Cafe.Models.KhachHang", b =>
                {
                    b.Property<string>("KhachHangID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("KhachHangName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("KhachHangID");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("Cafe.Models.NhanVien", b =>
                {
                    b.Property<string>("NhanVienID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NhanVienName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NhanVienID");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("Cafe.Models.SanPham", b =>
                {
                    b.Property<string>("SanPhamID")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Gia")
                        .HasColumnType("TEXT");

                    b.Property<string>("SanPhamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SoLuong")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SanPhamID");

                    b.ToTable("SanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}