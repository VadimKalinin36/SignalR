using Microsoft.AspNetCore.SignalR;
using WebChat.Services;


namespace WebChat.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
       // public string Recipient { get; set; }
        public string MessageText { get; set; }
        public DateTime DateSend { get; set; }
    }
}
