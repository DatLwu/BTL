
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
        public string SanPhamId { get; set; }
        public string SoLuong { get; set; }
        public string Gia { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
   
  
    }
}