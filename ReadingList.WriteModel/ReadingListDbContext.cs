using Microsoft.EntityFrameworkCore;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public sealed class ReadingListDbContext : DbContext
    {
        public DbSet<BookList> BookLists { get; set; }
        public DbSet<BookListItem> BookListItems { get; set; }
        public DbSet<PrivateBookList> PrivateBookLists { get; set; }
        public DbSet<SharedBookList> SharedBookLists { get; set; }
        public DbSet<PrivateBookListItem> PrivateBookListItems { get; set; }
        public DbSet<SharedBookListItem> SharedBookListItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=.\SQLEXPRESS; Initial Catalog=ReadingList; User Id=sa;Password=1324493; MultipleActiveResultSets=True;");
        }
    }
}