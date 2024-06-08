using Microsoft.EntityFrameworkCore;
using System;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class ChatRepository : Repository<ChatMessage>, IChatRepository

    {
        private readonly ModelContext _context;
        public ChatRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageVM>> GetMessageByUser(ApplicationUser currentUser, ApplicationUser adminUser)
        {
            var messages = await _context.ChatMessage.AsNoTracking()
                .Where(m => (m.SenderId == currentUser.Id && m.RecipientId == adminUser.Id) ||
                            (m.RecipientId == currentUser.Id && m.SenderId == adminUser.Id))
                .OrderBy(m => m.Time)
                .Select(m => new MessageVM
                {
                    Id = m.Id,
                    Content = m.Content,
                    Time = m.Time,
                    UserName = m.Sender.UserName,
                    Recipient = m.Recipient
                })
                .ToListAsync();

            return messages;
        }
    }
}
