using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }
        public string? Name{ get; set; }
    }
}

