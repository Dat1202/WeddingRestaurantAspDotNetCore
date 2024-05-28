using AutoMapper;
using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Controllers
{
    [Authorize(Roles = "User")]
    public class ChatController : Controller
    {
        private readonly ModelContext _model;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ModelContext model, UserManager<ApplicationUser> userManager
            , IMapper mapper) {
            _model = model;
            _mapper=mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var adminUsers = await _userManager.FindByNameAsync("admin");
            var messages = _model.ChatMessage
                .Where(m => m.SenderId == currentUser.Id && m.RecipientId == adminUsers.Id ||
                m.RecipientId == currentUser.Id && m.SenderId == adminUsers.Id)
                .OrderBy(m => m.Time)
                .Select(m => new MessageVM
                {
                    Id = m.Id,
                    Content = m.Content,
                    Time = m.Time,
                    UserName = m.Sender.UserName,
                    Recipient = m.Recipient
                }); 

            ViewBag.User = currentUser;
            ViewBag.admin = adminUsers;
            return View(messages);
        }
        public async Task<IActionResult> CreateMessage(string content, string? receiver)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var adminUsers = await _userManager.FindByNameAsync("admin");
                var message = new ChatMessage
                {
                    Content = content,
                    Time = DateTime.Now,
                    UserName = currentUser.UserName,
                    Sender = currentUser,
                };

                if(receiver != null)
                {
                    var receiverUser = await _userManager.FindByNameAsync(receiver);

                    message.Recipient = receiverUser;
                }
                else
                {
                    message.Recipient = adminUsers;
                }
                await _model.ChatMessage.AddAsync(message);
                await _model.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

    }
}
