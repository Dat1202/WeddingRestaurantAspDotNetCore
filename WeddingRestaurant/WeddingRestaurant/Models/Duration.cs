using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Duration
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public bool IsWeekend { get; set; } = false;
        [Required]
        public DateTime Time { get; set; }
    }
}
