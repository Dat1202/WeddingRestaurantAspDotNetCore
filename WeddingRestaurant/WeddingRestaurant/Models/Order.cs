using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentMethods {  get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
