using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class Event
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [StringLength(50)]
        public string? Name{ get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Capacity { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room? Room { get; set; }

        [ForeignKey("User")]
        public virtual ApplicationUser? UserId { get; set; }
    }
}

