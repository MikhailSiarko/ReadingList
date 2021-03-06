using System.Collections.Generic;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedList : Update<SharedBookListDto>
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly IEnumerable<Tag> Tags;

        public readonly IEnumerable<int> Moderators;

        public UpdateSharedList(int userId, int listId, string name, IEnumerable<Tag> tags,
            IEnumerable<int> moderators) : base(userId)
        {
            ListId = listId;
            Name = name;
            Tags = tags;
            Moderators = moderators;
        }
    }
}