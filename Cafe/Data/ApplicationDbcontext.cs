using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data{
    public class ApplicationDbcontext : DbContext{
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> option) : base(option){}
        public DbSet<KhachHang> KhachHang { get ; set ;}
        public DbSet<Cafe.Models.HoaDon> HoaDon { get; set; } = default!;
        
    }
}