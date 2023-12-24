
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cafe.Models
{
    [Table("HoaDons")]
    public class HoaDon
    {
        [Key]
        public string HoaDonID { get; set; } 
        public string KhachHangID { get; set; }
        [ForeignKey("KhachHangID")]
        public KhachHang? KhachHang {get; set;}
        public string NhanVienID { get; set; }
        [ForeignKey("NhanVienID")]
        public NhanVien? NhanVien {get; set;}
        public string SanPhamID { get; set; }
        [ForeignKey("SanPhamID")]
        public SanPham? SanPham {get; set;}
        public string SoLuong { get; set; }
        public string Gia { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        public string? SanPhamName { get; internal set; }
    }
}