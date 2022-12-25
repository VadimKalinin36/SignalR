using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebChat.Services
{
    public class ChatService
    {

        public List<string> Connections { get; set; } = new List<string>();
        //public async Task runprocess()
        //{

        //    _context.HistoryModels.RemoveRange(_context.HistoryModels);
        //    _context.SaveChanges();
        //}


    }
}
