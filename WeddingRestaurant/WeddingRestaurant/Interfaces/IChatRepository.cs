using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface IChatRepository : IRepository<ChatMessage>
    {
        Task<IEnumerable<MessageVM>> GetMessageByUser(ApplicationUser currentUser, ApplicationUser adminUser);
    }
}
