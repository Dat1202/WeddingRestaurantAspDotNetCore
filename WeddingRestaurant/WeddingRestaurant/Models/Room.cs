using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class Room
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [StringLength(50)]
        public string Name{ get; set; }
		[Column(TypeName = "decimal(10, 2)")]
		public decimal Price { get; set; }
        public int Location { get; set; }
        public int Capacity { get; set; }
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
        public string? Image {  get; set; }
	}
}

