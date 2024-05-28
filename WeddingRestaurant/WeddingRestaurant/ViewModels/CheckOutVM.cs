using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.ViewModels
{
	public class CheckOutVM
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [MaxLength(50, ErrorMessage = "{0} Tối đa {1} kí tự")]
        public string? Name { get; set; }
        public string? Note { get; set; }
        [Required(ErrorMessage = "Chưa nhập Số điện thoại")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Chưa nhập Số điện thoại")]
        public int NumberTable { get; set; }
    }
}
