namespace ReadingList.Domain.Commands.SharedList
{
    public class UpdateSharedListCommand : SecuredCommand
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly string[] Tags;

        public readonly string Category;
        
        public UpdateSharedListCommand(string userLogin, int listId, string name, string[] tags, string category) : base(userLogin)
        {
            ListId = listId;
            Name = name;
            Tags = tags;
            Category = category;
        }
    }
}