using Microsoft.EntityFrameworkCore;
using SCData.Models;

namespace SpecialityCatalogWebApi.Data
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                new User { Id = 1, Name="Admin", Password="12345"},

                });
        }

    }
}
