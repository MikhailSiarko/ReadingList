using Microsoft.EntityFrameworkCore;
using ReadingList.WriteModel.Models;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel
{
    public sealed class WriteDbContext : DbContext
    {

        public DbSet<BookListWm> BookLists { get; set; }
        public DbSet<PrivateBookListItemWm> PrivateBookListItems { get; set; }
        public DbSet<SharedBookListItemWm> SharedBookListItems { get; set; }
        public DbSet<UserWm> Users { get; set; }
        public DbSet<ProfileWm> Profiles { get; set; }
        public DbSet<BookWm> Books { get; set; }
        public DbSet<TagWm> Tags { get; set; }
        public DbSet<GenreWm> Genres { get; set; }
        public DbSet<RoleWm> Roles { get; set; }
        public DbSet<BookTagWm> BookTags { get; set; }
        public DbSet<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }
        public DbSet<SharedBookListTagWm> SharedBookListTags { get; set; }

        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            modelBuilder.Entity<UserWm>().ToTable(nameof(Users)).HasIndex(u => u.Login).IsUnique();
            
            modelBuilder.Entity<ProfileWm>().ToTable(nameof(Profiles)).HasIndex(p => p.Email).IsUnique();
            
            modelBuilder.Entity<BookListWm>().ToTable(nameof(BookLists)).HasOne(b => b.Owner).WithMany(u => u.BookLists)
                .HasForeignKey(b => b.OwnerId);
            
            modelBuilder.Entity<BookWm>().ToTable(nameof(Books)).HasIndex(b => new {b.Title, b.Author}).IsUnique();
            
            modelBuilder.Entity<RoleWm>().ToTable(nameof(Roles)).HasIndex(r => r.Name).IsUnique();
            
            modelBuilder.Entity<TagWm>().ToTable(nameof(Tags)).HasIndex(t => t.Name).IsUnique();
            
            modelBuilder.Entity<GenreWm>().ToTable(nameof(Genres)).HasIndex(c => c.Id).IsUnique();

            modelBuilder.Entity<GenreWm>().HasOne(x => x.Parent).WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);

            modelBuilder.Entity<PrivateBookListItemWm>().ToTable(nameof(PrivateBookListItems));
            
            modelBuilder.Entity<SharedBookListItemWm>().ToTable(nameof(SharedBookListItems));

            modelBuilder.Entity<BookTagWm>().ToTable(nameof(BookTags)).HasKey(bt => new {bt.TagId, bt.BookId});
            
            modelBuilder.Entity<BookTagWm>().HasOne(bt => bt.Tag).WithMany(t => t.BookTags)
                .HasForeignKey(bt => bt.TagId);
            
            modelBuilder.Entity<BookTagWm>().HasOne(bt => bt.Book).WithMany(b => b.BookTags)
                .HasForeignKey(bt => bt.BookId);

            modelBuilder.Entity<SharedBookListItemTagWm>().ToTable(nameof(SharedBookListItemTags))
                .HasKey(st => new {st.SharedBookListItemId, st.TagId});
            
            modelBuilder.Entity<SharedBookListItemTagWm>().HasOne(st => st.SharedBookListItem)
                .WithMany(si => si.SharedBookListItemTags).HasForeignKey(st => st.SharedBookListItemId);
            
            modelBuilder.Entity<SharedBookListItemTagWm>().HasOne(st => st.Tag).WithMany(t => t.SharedBookListItemTags)
                .HasForeignKey(st => st.TagId);
            
            modelBuilder.Entity<SharedBookListTagWm>().ToTable(nameof(SharedBookListTags))
                .HasKey(st => new {st.SharedBookListId, st.TagId});
            
            modelBuilder.Entity<SharedBookListTagWm>().HasOne(st => st.SharedBookList)
                .WithMany(x => x.SharedBookListTags).HasForeignKey(st => st.SharedBookListId);
            
            modelBuilder.Entity<SharedBookListTagWm>().HasOne(st => st.Tag).WithMany(t => t.SharedBookListTags)
                .HasForeignKey(st => st.TagId);

            base.OnModelCreating(modelBuilder);
        }
    }
}