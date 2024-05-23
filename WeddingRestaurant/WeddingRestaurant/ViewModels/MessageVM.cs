using WeddingRestaurant.Models;

namespace WeddingRestaurant.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string RecipientId { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime Time { get; set; }

    }
}
