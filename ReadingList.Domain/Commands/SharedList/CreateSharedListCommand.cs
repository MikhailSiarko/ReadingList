namespace ReadingList.Domain.Commands.SharedList
{
    public class CreateSharedListCommand : SecuredCommand
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