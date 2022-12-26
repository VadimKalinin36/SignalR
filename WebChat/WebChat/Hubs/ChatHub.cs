using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebChat.Services;
using Microsoft.Data.Sqlite;
using WebChat.Models;
using AutoMapper;
using Hangfire;

namespace WebChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public object Id { get; private set; }

        public ChatHub(ChatService chatService, ApplicationContext context, IMapper mapper)
        {
            _chatService = chatService;
            _context = context;
            _mapper = mapper;
        }

        public async Task runprocess()
        {

            _context.HistoryModels.RemoveRange(_context.HistoryModels);
            _context.SaveChanges();
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

            var jobId = BackgroundJob.Enqueue(
                () => Console.WriteLine("Fire-and-forget!"));

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