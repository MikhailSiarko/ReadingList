using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

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
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<SharedBookListItemTag> SharedBookListItemTags { get; set; }
        public DbSet<ReadingJournalRecord> ReadingJournalRecords { get; set; }

        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<Profile>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<BookList>().HasOne(b => b.Owner).WithMany(u => u.BookLists)
                .HasForeignKey(b => b.OwnerId);
            modelBuilder.Entity<Book>().HasIndex(b => b.Title).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<ReadingJournalRecord>().HasOne(j => j.Item).WithMany(i => i.ReadingJournalRecords)
                .HasForeignKey(j => j.ItemId);

            modelBuilder.Entity<BookTag>().HasKey(bt => new {bt.TagId, bt.BookId});
            modelBuilder.Entity<BookTag>().HasOne(bt => bt.Tag).WithMany(t => t.BookTags).HasForeignKey(bt => bt.TagId);
            modelBuilder.Entity<BookTag>().HasOne(bt => bt.Book).WithMany(b => b.BookTags).HasForeignKey(bt => bt.BookId);

            modelBuilder.Entity<SharedBookListItemTag>().HasKey(st => new {st.SharedBookListItemId, st.TagId});
            modelBuilder.Entity<SharedBookListItemTag>().HasOne(st => st.SharedBookListItem)
                .WithMany(si => si.SharedBookListItemTags).HasForeignKey(st => st.SharedBookListItemId);
            modelBuilder.Entity<SharedBookListItemTag>().HasOne(st => st.Tag).WithMany(t => t.SharedBookListItemTags)
                .HasForeignKey(st => st.TagId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}