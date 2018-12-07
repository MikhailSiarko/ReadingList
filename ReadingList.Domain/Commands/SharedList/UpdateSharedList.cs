using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedList : Update<SharedBookListPreviewDto>
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly string[] Tags;

        public UpdateSharedList(int userId, int listId, string name, string[] tags) : base(userId)
        {
            ListId = listId;
            Name = name;
            Tags = tags;
        }
    }
}