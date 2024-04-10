using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Address{ get; set; }
        public string Phone{ get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }
}
