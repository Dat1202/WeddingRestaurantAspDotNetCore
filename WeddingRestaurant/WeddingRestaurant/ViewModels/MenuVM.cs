using WeddingRestaurant.Models;

namespace WeddingRestaurant.ViewModels
{
    public class MenuVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<int>? productIdsInCart { get; set; }
    }
}
