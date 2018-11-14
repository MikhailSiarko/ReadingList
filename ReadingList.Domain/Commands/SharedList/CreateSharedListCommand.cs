using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class CreateSharedListCommand : SecuredCommand<SharedBookListPreviewDto>
    {
        public readonly string Name;

        public readonly string[] Tags;
        
        public CreateSharedListCommand(int userId, string name, string[] tags) : base(userId)
        {
            Name = name;
            Tags = tags;
        }
    }
}