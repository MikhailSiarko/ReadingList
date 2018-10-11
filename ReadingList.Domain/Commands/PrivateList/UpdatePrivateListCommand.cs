using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateListCommand : UpdateCommand<PrivateBookListDto>
    {
        public readonly string Name;

        public UpdatePrivateListCommand(string login, string name) : base(login)
        {
            Name = name;
        }
    }
}