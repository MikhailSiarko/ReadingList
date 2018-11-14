using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingList.Domain.Models.DAO.HelpEntities;

namespace ReadingList.Write.Configurations
{
    public class BookListModeratorConfiguration : IEntityTypeConfiguration<BookListModerator>
    {
        public void Configure(EntityTypeBuilder<BookListModerator> builder)
        {
            builder.HasKey(m => new {m.UserId, m.BookListId});

            builder.HasOne(m => m.BookList).WithMany(m => m.BookListModerators).HasForeignKey(m => m.BookListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}