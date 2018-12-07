using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Write.Configurations
{
    public class SharedBookListTagConfiguration : IEntityTypeConfiguration<SharedBookListTag>
    {
        public void Configure(EntityTypeBuilder<SharedBookListTag> builder)
        {
            builder.HasKey(st => new {st.SharedBookListId, st.TagId});

            builder.HasOne(st => st.SharedBookList).WithMany(x => x.SharedBookListTags)
                .HasForeignKey(st => st.SharedBookListId);

            builder.HasOne(st => st.Tag).WithMany(t => t.SharedBookListTags).HasForeignKey(st => st.TagId);
        }
    }
}