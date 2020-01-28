using Core.Utilities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElevatorAdmin.Hubs
{
    public class ChatManageHub : Hub
    {
        public async Task SendMessageToAll(string message,string userId)
        {
            var connectionId = this.Context.ConnectionId;
            var User = this.Context.User.Identity.Name;
            var myId = this.Context.User.Identity.FindFirstValue(ClaimTypes.NameIdentifier);
            var ProfilePic = this.Context.User.Identity.FindFirstValue("UserProfile");

            await Clients.User(userId).SendAsync("ReciveMessage", message, connectionId,User, ProfilePic);
            await Clients.User(myId).SendAsync("ReciveMessage", message, connectionId,User, ProfilePic);

        }

        public override  Task OnConnectedAsync()
        {
            var connectionId = this.Context.ConnectionId;
            //var User = this.Context.User.Identity.FindFirstValue("FullName");
            //var ProfilePic = this.Context.User.Identity.FindFirstValue("UserProfile");

            //Clients.Users("3", "1").SendAsync("ReciveMessage", "سلام", connectionId, User, ProfilePic);
            //var test = Context.UserIdentifier;

            //Clients.User("3").SendAsync("ReciveMessage", "سلام", connectionId, User, ProfilePic);

            Clients.Client(connectionId).SendAsync("SetConnection", connectionId);

            return base.OnConnectedAsync();
        }
    }
}
