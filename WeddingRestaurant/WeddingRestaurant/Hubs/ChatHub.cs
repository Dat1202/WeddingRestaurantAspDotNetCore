using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WeddingRestaurant.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            return base.OnConnectedAsync();
        }
        public async Task SendMessageToUser(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
        }
    }
}
