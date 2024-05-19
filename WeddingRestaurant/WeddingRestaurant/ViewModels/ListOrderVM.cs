namespace WeddingRestaurant.ViewModels
{
    public class ListOrderVM
    {
        public int OrderId { get; set; }
        public string PaymentMethods { get; set; }
        public List<OrderDetailVM> OrderDetails { get; set; }

    }
}
