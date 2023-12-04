using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Models;
     [Table("Drinks")]
public class Drink
{  
    [Key]
    public string DrinkId { get; set; }
    public string DrinkName { get; set; }
    public string SoLuong { get; set; }
    public decimal Price { get; set; }
    public string BillId { get; set; }
    [ForeignKey("BillId")]
    public Bill? Bill {get; set; }
}