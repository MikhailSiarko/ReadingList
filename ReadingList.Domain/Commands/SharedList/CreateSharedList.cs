using System.Collections.Generic;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Commands
{
    public class CreateSharedList : SecuredCommand<SharedBookListPreviewDto>
    {
        public readonly string Name;

        public readonly IEnumerable<Tag> Tags;

        public CreateSharedList(int userId, string name, IEnumerable<Tag> tags) : base(userId)
        {
            Name = name;
            Tags = tags;
        }
    }
}