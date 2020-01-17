using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            var connectionId = this.Context.ConnectionId;
            await Clients.All.SendAsync("ReciveMessage", message, connectionId);
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = this.Context.ConnectionId;
            Clients.Client(connectionId).SendAsync("SetConnection", connectionId);

            return base.OnConnectedAsync();
        }
    }
}
