using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data{
    public class ApplicationDbcontext : DbContext{
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> option) : base(option){}
        public DbSet<KhachHang> KhachHang { get ; set ;}
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<NhanVien> NhanVien {get; set ;}
        public DbSet<SanPham> SanPham {get; set ;}

        
    }
}