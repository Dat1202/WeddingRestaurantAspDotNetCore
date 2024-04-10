using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Duration
    {
        [Key]
        public int Id{ get; set; }
        public bool IsWeekend{ get; set; }
        public DateTime Time { get; set; }
    }
}
