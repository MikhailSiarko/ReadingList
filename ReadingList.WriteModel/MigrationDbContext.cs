using Microsoft.EntityFrameworkCore;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public class MigrationDbContext : DbContext
    {
        public DbSet<BookList> BookLists { get; set; }
        public DbSet<PrivateBookListItem> PrivateBookListItems { get; set; }
        public DbSet<SharedBookListItem> SharedBookListItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public MigrationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<Profile>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<BookList>().HasOne(b => b.Owner).WithMany(u => u.BookLists)
                .HasForeignKey(b => b.OwnerId);
            modelBuilder.Entity<Book>().HasIndex(b => b.Title).IsUnique();
            modelBuilder.Entity<Book>().HasIndex(b => b.Author).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}