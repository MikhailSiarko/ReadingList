using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedListCommand : UpdateCommand<SharedBookListPreviewDto>
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly string[] Tags;
        
        public UpdateSharedListCommand(int userId, int listId, string name, string[] tags) : base(userId)
        {
            ListId = listId;
            Name = name;
            Tags = tags;
        }
    }
}