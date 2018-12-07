using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class CreateSharedList : SecuredCommand<SharedBookListPreviewDto>
    {
        public readonly string Name;

        public readonly string[] Tags;

        public CreateSharedList(int userId, string name, string[] tags) : base(userId)
        {
            Name = name;
            Tags = tags;
        }
    }
}