using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateListCommand : UpdateCommand<PrivateBookListDto>
    {
        public readonly string Name;

        public UpdatePrivateListCommand(int userId, string name) : base(userId)
        {
            Name = name;
        }
    }
}