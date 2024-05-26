using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
		[StringLength(70)]
        public string? Avatar { get; set; }

    }
}
