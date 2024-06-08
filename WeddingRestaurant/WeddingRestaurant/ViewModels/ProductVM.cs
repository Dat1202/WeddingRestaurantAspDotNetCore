namespace WeddingRestaurant.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<int>? ProductIds { get; set; }

    }
}
