using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CartItem
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public double TotalPrice => Quantity * Price;
    }
}
