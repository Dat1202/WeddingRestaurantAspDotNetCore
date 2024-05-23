using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(50, ErrorMessage = "{0} Tối đa {1} kí tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [MaxLength(50, ErrorMessage = "{0} Tối đa {1} kí tự")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])(?=.*[0-9]).{6,}$", ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one special character, and one digit.")]
        public string Password { get; set; }
    }
}
