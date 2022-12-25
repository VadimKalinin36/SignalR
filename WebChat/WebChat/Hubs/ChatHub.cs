using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebChat.Services;
using Microsoft.Data.Sqlite;
using WebChat.Models;

namespace WebChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly ApplicationContext _context;

        public ChatHub(ChatService chatService, ApplicationContext context)
        {
            _chatService = chatService;
            _context = context;
        }

        public async Task Enter(string userName)
        {
            await Clients.All.SendAsync("Notify", $"{userName} вошел в чат");
        }

        public async Task CreateMessage(string message, string userName, string? connectionId)
        {

            var HistoryModel = new HistoryModel { Sender = userName, MessageText = message, DateSend = DateTime.Now };
            _context.HistoryModels.Add(HistoryModel);
            _context.SaveChanges();

            if (String.IsNullOrEmpty(userName))
            {
                await Clients.Caller.SendAsync("Notify", "Введите логин");
            }
            else
            {
                if (connectionId != null)
                {
                    await Clients.Clients(connectionId).SendAsync("SendMessage", message, userName, Context.ConnectionId);
                    await Clients.Caller.SendAsync("SendMessage", message, userName, Context.ConnectionId);
                }
                else
                {
                    await Clients.All.SendAsync("SendMessage", message, userName, Context.ConnectionId);
                }
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");

            await base.OnConnectedAsync();


        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул чат");

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Invite(string connectionId)
        {
            if (connectionId == Context.ConnectionId) return;

            await Clients.Client(connectionId).SendAsync("SendInvite", Context.ConnectionId);
        }

        public async Task Out(string connectionId)
        {
            if (connectionId == Context.ConnectionId) return;

            await Clients.Client(connectionId).SendAsync("Logout", Context.ConnectionId);

        }

    }
}