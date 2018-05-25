using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public sealed class WriteDbContext : DbContext
    {

        private readonly IConfiguration _configuration;
        
        public DbSet<BookList> BookLists { get; set; }
        public DbSet<PrivateBookListItem> PrivateBookListItems { get; set; }
        public DbSet<SharedBookListItem> SharedBookListItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }

        public WriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }
    }
}