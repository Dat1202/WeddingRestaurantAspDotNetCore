using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User? user { get; set; }
    }
}
