using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.ViewModels
{
    public class ExternalLoginVM
    {
        public string? Provider { get; set; }
        public string? ReturnUrl { get; set; }
        public string Email { get; set; }
        public string? Fullname { get; set; }
    }
}
