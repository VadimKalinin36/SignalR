using Microsoft.EntityFrameworkCore;
using WebChat.Models;

namespace WebChat
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HistoryModel> HistoryModels { get; set; }
        public ApplicationContext() => Database.EnsureCreated();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
