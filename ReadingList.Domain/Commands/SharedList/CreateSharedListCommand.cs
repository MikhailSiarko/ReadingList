namespace ReadingList.Domain.Commands.SharedList
{
    public class CreateSharedListCommand : SecuredCommand
    {
        public readonly string Name;
        
        public CreateSharedListCommand(string userLogin, string name) : base(userLogin)
        {
            Name = name;
        }
    }
}