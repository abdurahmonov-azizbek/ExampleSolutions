using Microsoft.AspNetCore.SignalR;

namespace FRM.WebApi.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await this.Clients.All.SendAsync("ReceiveMessage", user, message);  
    }
}
