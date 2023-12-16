
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cafe.Models
{
    [Table("KhachHangs")]
    public class KhachHang
    {
        [Key]
        public string KhachHangID { get; set; } 
        public string KhachHangName { get; set; }
        public string Address { get; set; }
        public string SDT { get; set; }
        public string Email {get; set;}
        public ICollection<HoaDon>? HoaDon {get; set ;}

    }
}