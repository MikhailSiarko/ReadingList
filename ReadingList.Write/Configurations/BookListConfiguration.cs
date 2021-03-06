using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingList.Models.Write;

namespace ReadingList.Write.Configurations
{
    public class BookListConfiguration : IEntityTypeConfiguration<BookList>
    {
        public void Configure(EntityTypeBuilder<BookList> builder)
        {
            builder.HasOne(b => b.Owner).WithMany(u => u.BookLists).HasForeignKey(b => b.OwnerId);
        }
    }
}