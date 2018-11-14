using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.HelpEntities;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Write.Configurations;

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
        
//        public async Task<IEnumerable<SharedBookListTag>> UpdateOrAddSharedListTags(IEnumerable<string> tags, BookList list)
//        {
//            if (list.SharedBookListTags != null)
//            {
//                SharedBookListTags.RemoveRange(list.SharedBookListTags);
//
//                await SaveChangesAsync();
//            }
//
//            var existingTags = await Tags.Where(x => tags.Contains(x.Name)).ToListAsync();
//
//            var newTags = tags.Where(x => !Tags.Any(y => y.Name == x)).Select(x => new Tag
//            {
//                Name = x
//            }).ToList();
//
//            await Tags.AddRangeAsync(newTags);
//
//            return existingTags.Concat(newTags).Select(t => new SharedBookListTag
//            {
//                TagId = t.Id,
//                SharedBookListId = list.Id
//            });
//        }
//
//        public async Task<IEnumerable<SharedBookListItemTag>> UpdateOrAddSharedListItemTags(IEnumerable<string> tags, SharedBookListItem item)
//        {
//            if (item.SharedBookListItemTags != null)
//            {
//                SharedBookListItemTags.RemoveRange(item.SharedBookListItemTags);
//
//                await SaveChangesAsync();
//            }
//
//            var existingTags = await Tags.Where(x => tags.Contains(x.Name)).ToListAsync();
//
//            var newTags = tags.Where(x => !Tags.Any(y => y.Name == x)).Select(x => new Tag
//            {
//                Name = x
//            }).ToList();
//
//            await Tags.AddRangeAsync(newTags);
//
//            return existingTags.Concat(newTags).Select(t => new SharedBookListItemTag
//            {
//                TagId = t.Id,
//                SharedBookListItemId = item.Id
//            });
//        }
    }
}