using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Commands
{
    public class CreateSharedListCommand : SecuredCommand<SimplifiedSharedBookListDto>
    {
        public readonly string Name;

        public readonly string[] Tags;
        
        public CreateSharedListCommand(string userLogin, string name, string[] tags) : base(userLogin)
        {
            Name = name;
            Tags = tags;
        }
    }
}