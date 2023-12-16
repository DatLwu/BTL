using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe.Migrations
{
    /// <inheritdoc />
    public partial class KhachHang_Create_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhachHangID = table.Column<string>(type: "TEXT", nullable: false),
                    KhachHangName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    SDT = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhachHangID);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    NhanVienID = table.Column<string>(type: "TEXT", nullable: false),
                    NhanVienName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    SDT = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.NhanVienID);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    SanPhamID = table.Column<string>(type: "TEXT", nullable: false),
                    SanPhamName = table.Column<string>(type: "TEXT", nullable: false),
                    SoLuong = table.Column<string>(type: "TEXT", nullable: false),
                    Gia = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.SanPhamID);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    HoaDonID = table.Column<string>(type: "TEXT", nullable: false),
                    KhachHangID = table.Column<string>(type: "TEXT", nullable: false),
                    NhanVienID = table.Column<string>(type: "TEXT", nullable: false),
                    SanPhamID = table.Column<string>(type: "TEXT", nullable: false),
                    SoLuong = table.Column<string>(type: "TEXT", nullable: false),
                    Gia = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.HoaDonID);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhachHangs_KhachHangID",
                        column: x => x.KhachHangID,
                        principalTable: "KhachHangs",
                        principalColumn: "KhachHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanViens",
                        principalColumn: "NhanVienID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDons_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_KhachHangID",
                table: "HoaDons",
                column: "KhachHangID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_NhanVienID",
                table: "HoaDons",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_SanPhamID",
                table: "HoaDons",
                column: "SanPhamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "SanPhams");
        }
    }
}
