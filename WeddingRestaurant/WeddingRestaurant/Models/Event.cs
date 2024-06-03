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
        public string Name { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int NumberTable { get; set; }
        public string? Note { get; set; }
        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public virtual Room? Room { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}

