using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public sealed class ReadingListDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ReadingListDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
        public DbSet<BookItemStatus> BookItemStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookItemStatus>().ToTable("BookItemStatuses");
            modelBuilder.Entity<Profile>().HasKey(p => p.UserId);
            modelBuilder.Entity<Profile>().HasOne(p => p.User).WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<Profile>().HasIndex(p => p.Email).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}