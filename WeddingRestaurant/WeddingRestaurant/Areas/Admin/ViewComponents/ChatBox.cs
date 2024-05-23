using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.ViewComponents
{
    public class ChatBox : ViewComponent
    {
        private readonly ModelContext _model;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatBox(ModelContext model, UserManager<ApplicationUser> userManager)
        {
            _model = model;
            _userManager = userManager;

        }
        public async Task<IViewComponentResult> InvokeAsync(string? userId)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            var adminUsers = await _userManager.FindByNameAsync("admin");
            var messages = _model.ChatMessage
                .Where(m => m.SenderId == adminUsers.Id && m.RecipientId == userId ||
                m.RecipientId == adminUsers.Id && m.SenderId == userId)
                .OrderBy(m => m.Time)
                .Select(m => new MessageVM
                {
                    Id = m.Id,
                    Content = m.Content,
                    Time = m.Time,
                    UserName = m.Sender.UserName,
                    Recipient = m.Recipient
                });

            //ViewBag.User = currentUser;
            ViewBag.admin = adminUsers;

            return View("ChatBoxContent", messages);
        }

    }
}
