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
        [ForeignKey("ApplicationUser")]
        public virtual ApplicationUser? UserId { get; set; }
    }
}
