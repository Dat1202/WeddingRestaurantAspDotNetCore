using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(30)]
        public string Avatar { get; set; }

    }
}
