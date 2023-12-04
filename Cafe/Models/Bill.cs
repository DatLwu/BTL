
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cafe.Models
{
   
    public class Bill
    {
     
        public string BillId { get; set; } 
        public string TenKH { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        public string MaNV { get; set; }
        public ICollection<Drink>? Drink {get; set;}
    }
}