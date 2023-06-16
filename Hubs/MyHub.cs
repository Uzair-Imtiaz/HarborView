using Microsoft.AspNetCore.SignalR;
using HarborView_Inn.Models;

namespace HarborView_Inn.Hubs
{
    public class MyHub : Hub
    {
        
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message); 
        }

    }
}
