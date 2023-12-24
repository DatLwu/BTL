
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cafe.Models
{
    [Table("NhanViens")]
    public class NhanVien
    {
        [Key]
        public string NhanVienID { get; set; } 
        public string NhanVienName { get; set; }
        public string Address { get; set; }
        public string SDT { get; set; }
        public string Email {get; set;}
        //public ICollection<HoaDon>? HoaDon {get; set ;}
  
    }
}