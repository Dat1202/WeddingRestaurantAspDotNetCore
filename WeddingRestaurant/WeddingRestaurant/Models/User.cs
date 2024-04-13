using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Avatar { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone{ get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }
}
