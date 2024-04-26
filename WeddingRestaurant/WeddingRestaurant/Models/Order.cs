using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace WeddingRestaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentMethods {  get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser? Users { get; set; }
    }
}
