using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class RentCost
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room? Room { get; set; }                      

        [ForeignKey("Duration")]
        public int DurationId { get; set; }
        public virtual Duration? Duration { get; set; }
    }
}
