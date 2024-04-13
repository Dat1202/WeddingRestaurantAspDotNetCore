using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Room
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [StringLength(50)]
        public string Name{ get; set; }
        public int Location { get; set; }
        public int Capacity { get; set; }
    }
}

