using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Write.Configurations
{
    public class BookTagConfiguration : IEntityTypeConfiguration<BookTag>
    {
        public void Configure(EntityTypeBuilder<BookTag> builder)
        {
            builder.HasKey(bt => new {bt.TagId, bt.BookId});

            builder.HasOne(bt => bt.Tag).WithMany(t => t.BookTags).HasForeignKey(bt => bt.TagId);

            builder.HasOne(bt => bt.Book).WithMany(b => b.BookTags).HasForeignKey(bt => bt.BookId);
        }
    }
}