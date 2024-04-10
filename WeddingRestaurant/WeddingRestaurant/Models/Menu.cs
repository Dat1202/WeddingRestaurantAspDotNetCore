using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Menu
    {
        [Key]
        public int Id{ get; set; }
        public string? Name{ get; set; }
    }
}
