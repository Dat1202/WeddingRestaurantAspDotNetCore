using AutoMapper;
using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class ChatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = Configuration.RoleUser)]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var adminUser = await _userManager.FindByNameAsync("admin");

            return View(await _unitOfWork.Chats.GetMessageByUser(currentUser, adminUser));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(string content, string? receiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var message = new ChatMessage
            {
                Content = content,
                Time = DateTime.Now,
                UserName = currentUser.UserName,
                Sender = currentUser,
            };

            if (!string.IsNullOrEmpty(receiver))
            {
                var receiverUser = await _userManager.FindByNameAsync(receiver);
                if (receiverUser == null)
                {
                    return NotFound("Receiver not found");
                }
                message.Recipient = receiverUser;
            }
            else
            {
                var adminUser = await _userManager.FindByNameAsync("admin");
                if (adminUser == null)
                {
                    return NotFound("Admin user not found");
                }
                message.Recipient = adminUser;
            }

            await _unitOfWork.Chats.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
