using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class Menu
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [StringLength(50)]
        public string Name{ get; set; }
        [ForeignKey("TypeMenu")]
        public int TypeID { get; set; }
        public virtual TypeMenu? TypeMenu { get; set; }
        //public ICollection<Product>? Products { get; set; }
    }
}
