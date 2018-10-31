using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Commands
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