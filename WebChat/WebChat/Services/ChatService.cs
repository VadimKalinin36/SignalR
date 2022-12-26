using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebChat.Hubs;

namespace WebChat.Services
{
    public class ChatService
    {
        private readonly ApplicationContext _context;

        public List<string> Connections { get; set; } = new List<string>();

        public ChatService(ApplicationContext context)
        {
            _context = context;
        }

        public void ClearChat()
        {
            var messages = _context.HistoryModels.Where(x => x.DateSend < DateTime.Now.AddMinutes(-1)).ToList();
            if (messages.Any())
            {
                _context.RemoveRange(messages);
                _context.SaveChanges();
            }
        }
    }
}
