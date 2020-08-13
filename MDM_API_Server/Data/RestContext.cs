
using Microsoft.EntityFrameworkCore;
using Rest.Models;

namespace Rest.Data 
{
    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> opt) : base(opt) {}

        public DbSet<User> Users { get; set; }

        public DbSet<PodcastFeed> PodcastFeed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PodcastFeed>()
                .Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}