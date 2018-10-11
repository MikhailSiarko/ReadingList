using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedListCommand : UpdateCommand<SharedBookListDto>
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly string[] Tags;
        
        public UpdateSharedListCommand(string userLogin, int listId, string name, string[] tags) : base(userLogin)
        {
            ListId = listId;
            Name = name;
            Tags = tags;
        }
    }
}