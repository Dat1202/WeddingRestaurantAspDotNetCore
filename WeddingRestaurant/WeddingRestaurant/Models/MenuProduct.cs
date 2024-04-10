using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class MenuProduct
    {
        [Key]
        public int Id{ get; set; }
        
        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public virtual Menu? Menu { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}

