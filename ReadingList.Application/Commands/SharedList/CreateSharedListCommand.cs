using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Commands
{
    public class CreateSharedListCommand : SecuredCommand<SharedBookListPreviewDto>
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