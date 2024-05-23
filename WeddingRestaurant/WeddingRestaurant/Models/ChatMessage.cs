namespace WeddingRestaurant.Models
{
    public class ChatMessage
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
