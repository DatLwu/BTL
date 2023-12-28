using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Models;
[Table("SanPhams")]
public class SanPham
{  
    [Key]
    public string SanPhamID { get; set; }
    public string SanPhamName { get; set; }
    public string SoLuong { get; set; }
    public string Gia { get; set; }
    public ICollection<HoaDon>? HoaDon {get; set ;}
   
}