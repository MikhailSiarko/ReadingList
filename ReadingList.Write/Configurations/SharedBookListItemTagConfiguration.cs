using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingList.Domain.Entities.HelpEntities;

namespace ReadingList.Write.Configurations
{
    public class SharedBookListItemTagConfiguration : IEntityTypeConfiguration<SharedBookListItemTag>
    {
        public void Configure(EntityTypeBuilder<SharedBookListItemTag> builder)
        {
            builder.HasKey(st => new {st.SharedBookListItemId, st.TagId});

            builder.HasOne(st => st.SharedBookListItem).WithMany(si => si.SharedBookListItemTags)
                .HasForeignKey(st => st.SharedBookListItemId);

            builder.HasOne(st => st.Tag).WithMany(t => t.SharedBookListItemTags).HasForeignKey(st => st.TagId);
        }
    }
}