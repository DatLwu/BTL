
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cafe.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

    }
}