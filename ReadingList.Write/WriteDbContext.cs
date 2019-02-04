using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReadingList.Models;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;
using ReadingList.Models.Write.Identity;
using ReadingList.Write.Configurations;
using ReadingList.Write.Infrastructure;

namespace ReadingList.Write
{
    public sealed class WriteDbContext : DbContext
    {
        public DbSet<BookList> BookLists { get; set; }
        public DbSet<PrivateBookListItem> PrivateBookListItems { get; set; }
        public DbSet<SharedBookListItem> SharedBookListItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<SharedBookListTag> SharedBookListTags { get; set; }
        public DbSet<BookListModerator> BookListModerators { get; set; }

        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        public IQueryable<T> Table<T>() where T : Entity =>
            Set<T>().Include(this.GetIncludePaths<T>());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookListModeratorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new BookListConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new BookTagConfiguration());
            modelBuilder.ApplyConfiguration(new SharedBookListTagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}