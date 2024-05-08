using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "*")]
        //[EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "{0} Tối đa {1} kí tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "{0} Tối đa {1} kí tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Số điện thoại")]
        [MaxLength(20, ErrorMessage = "{0} Tối đa {1} kí tự")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [MaxLength(20, ErrorMessage = "{0} Tối đa {1} kí tự")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])(?=.*[0-9]).{6,}$", ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one special character, and one digit.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [MaxLength(20, ErrorMessage = "{0} Tối đa {1} kí tự")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Avatar { get; set; }

    }
}
