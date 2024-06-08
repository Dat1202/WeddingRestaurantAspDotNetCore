using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Configuration.RoleAdmin)]
    public class ChatController : Controller
    {
        private readonly ModelContext _model;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ModelContext model, UserManager<ApplicationUser> userManager) 
        {
            _model = model; 
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var distinctUsernames = await _model.ChatMessage
                                         .Where(cm => cm.Recipient == currentUser)
                                         .Select(cm => cm.UserName)
                                         .Distinct()
                                         .ToListAsync();


            if (currentUser != null)
            {
                var topContent = await _model.ChatMessage
                                        .Where(cm => distinctUsernames.Contains(cm.UserName) && cm.Recipient == currentUser)
                                        .GroupBy(cm => cm.UserName)
                                        .Select(group => group.OrderByDescending(cm => cm.Time).First())
                                        .ToListAsync();

                return View(topContent);
            }
            return View();
        }

        public async Task<IActionResult> DetailMessage(string? userName)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var sentUser = await _userManager.FindByNameAsync(userName);
            if (currentUser != null)
            {
                var messages = _model.ChatMessage
                .Where(m => m.SenderId == currentUser.Id && m.Recipient == sentUser ||
                m.RecipientId == currentUser.Id && m.Sender == sentUser)
                .OrderBy(m => m.Time)
                .Select(m => new MessageVM
                {
                    Id = m.Id,
                    Content = m.Content,
                    Time = m.Time,
                    UserName = m.Sender.UserName,
                    Recipient = m.Recipient
                }); 
                return View(messages);
            }

            return View();
        }
    }
}
