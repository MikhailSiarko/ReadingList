namespace ReadingList.Domain.Commands.SharedList
{
    public class CreateSharedListCommand : SecuredCommand
    {
        public readonly string Name;

        public readonly string[] Tags;

        public readonly string Category;
        
        public CreateSharedListCommand(string userLogin, string name, string[] tags, string category) : base(userLogin)
        {
            Name = name;
            Tags = tags;
            Category = category;
        }
    }
}