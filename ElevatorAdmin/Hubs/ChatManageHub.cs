using Core.Utilities;
using Microsoft.AspNetCore.SignalR;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElevatorAdmin.Hubs
{
    public class ChatManageHub : Hub
    {
        private readonly UserRepository userRepository;

        public ChatManageHub(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task SendMessageToAll(string message,string userId)
        {
            var connectionId = this.Context.ConnectionId;
            var User = this.Context.User.Identity.Name;
            var myId = this.Context.User.Identity.FindFirstValue(ClaimTypes.NameIdentifier);
            var ProfilePic = this.Context.User.Identity.FindFirstValue("UserProfile");

            await Clients.User(userId).SendAsync("ReciveMessage", message, connectionId,User, ProfilePic);
            await Clients.User(myId).SendAsync("ReciveMessage", message, connectionId,User, ProfilePic);

        }


        public async Task SetContactChat(string userId)
        {
            var userInfo = userRepository.GetById(int.Parse(userId));
            var myId = this.Context.User.Identity.FindFirstValue(ClaimTypes.NameIdentifier);
            await Clients.User(myId).SendAsync("SetContact",userInfo);
        }


        public override  Task OnConnectedAsync()
        {
            var connectionId = this.Context.ConnectionId;


            Clients.Client(connectionId).SendAsync("SetConnection", connectionId);

            return base.OnConnectedAsync();
        }
    }
}
