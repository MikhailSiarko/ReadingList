﻿using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.HelpEntities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Write.Configurations;

namespace ReadingList.Write
{
    public sealed class ApplicationDbContext : DbContext
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
        public DbSet<SharedBookListItemTag> SharedBookListItemTags { get; set; }
        public DbSet<SharedBookListTag> SharedBookListTags { get; set; }
        public DbSet<BookListModerator> BookListModerators { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
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
            modelBuilder.ApplyConfiguration(new SharedBookListItemTagConfiguration());
            modelBuilder.ApplyConfiguration(new SharedBookListTagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}